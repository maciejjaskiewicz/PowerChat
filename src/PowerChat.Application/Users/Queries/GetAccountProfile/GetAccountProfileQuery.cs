using MediatR;
using PowerChat.Application.Users.Queries.GetAccountProfile.Models;

namespace PowerChat.Application.Users.Queries.GetAccountProfile
{
    public class GetAccountProfileQuery : IRequest<AccountProfileModel>
    {
    }
}
