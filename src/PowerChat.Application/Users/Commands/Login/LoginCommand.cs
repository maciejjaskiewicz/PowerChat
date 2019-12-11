using MediatR;
using PowerChat.Application.Common.Results;
using PowerChat.Application.Users.Dto;

namespace PowerChat.Application.Users.Commands.Login
{
    public class LoginCommand : IRequest<ApplicationResult<JwtDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
