using MediatR;
using PowerChat.Application.Common.Results;

namespace PowerChat.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<ApplicationResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
