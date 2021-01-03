using MediatR;
using PowerChat.Services.Common.Application.Results;

namespace PowerChat.Services.Identity.Application.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<ApplicationResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
    }
}