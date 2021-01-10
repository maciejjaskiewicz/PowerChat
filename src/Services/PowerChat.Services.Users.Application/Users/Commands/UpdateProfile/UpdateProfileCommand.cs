using MediatR;
using PowerChat.Services.Common.Application.Results;

namespace PowerChat.Services.Users.Application.Users.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<ApplicationResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }
    }
}
