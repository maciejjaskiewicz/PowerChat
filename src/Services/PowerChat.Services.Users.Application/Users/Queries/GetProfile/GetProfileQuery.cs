using MediatR;
using PowerChat.Services.Users.Application.Users.Queries.GetProfile.Models;

namespace PowerChat.Services.Users.Application.Users.Queries.GetProfile
{
    public class GetProfileQuery : IRequest<UserProfileModel>
    {
    }
}
