using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PowerChat.Application.Users.Dto;
using PowerChat.Common;
using PowerChat.Common.Extensions;

namespace PowerChat.Infrastructure.Identity
{
    public interface IJwtGenerator : IInfrastructureService
    {
        JwtDto Generate(long userId, string email);
    }

    public class JwtGenerator : IJwtGenerator
    {
        private readonly IDateTime _dateTime;
        private readonly IConfiguration _configuration;

        public JwtGenerator(IDateTime dateTime, IConfiguration configuration)
        {
            _dateTime = dateTime;
            _configuration = configuration;
        }

        public JwtDto Generate(long userId, string email)
        {
            var now = _dateTime.UtcNow;
            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString(), ClaimValueTypes.Integer64)
            };

            var expires = now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"]));
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expires = expires.ToTimestamp()
            };
        }
    }
}
