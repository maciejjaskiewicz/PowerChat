using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Common.Results;
using PowerChat.Application.Users.Dto;
using PowerChat.Common.Results;
using PowerChat.Domain.Entities;

namespace PowerChat.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthService(UserManager<User> userManager,
            SignInManager<User> signInManager, 
            IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<ApplicationResult<JwtDto>> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var loginResult = await _signInManager.PasswordSignInAsync(email, password, false, false);
            
            if (loginResult.Succeeded)
            {
                var token = _jwtGenerator.Generate(user.Id, email);
                return ApplicationResult<JwtDto>.Ok(token);
            }

            return TranslateSignInResult(loginResult, email);
        }

        private ApplicationResult<JwtDto> TranslateSignInResult(SignInResult signInResult, string email)
        {
            if (signInResult.IsLockedOut)
            {
                return ApplicationResult<JwtDto>.Fail(PowerChatError.Create(IdentityErrorCodes.UserLockedOut, 
                    $"User with email '{email}' is locked out."));
            }

            return ApplicationResult<JwtDto>.Fail(PowerChatError.Create(IdentityErrorCodes.InvalidCredentials, 
                "Invalid credentials. Please try again."));
        }
    }
}
