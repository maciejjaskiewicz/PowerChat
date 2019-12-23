using MediatR;
using PowerChat.Application.Common.Results;

namespace PowerChat.Application.Users.Commands.UpdateAccountProfile
{
    public class UpdateAccountProfileCommand : IRequest<ApplicationResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }
    }
}
