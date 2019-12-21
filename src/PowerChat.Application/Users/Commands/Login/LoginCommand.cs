using MediatR;
using PowerChat.Application.Common.Results;
using PowerChat.Application.Users.Models;

namespace PowerChat.Application.Users.Commands.Login
{
    public class LoginCommand : IRequest<ApplicationResult<LoginResponseModel>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
