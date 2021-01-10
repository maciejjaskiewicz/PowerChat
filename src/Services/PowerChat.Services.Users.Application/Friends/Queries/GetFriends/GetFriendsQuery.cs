using System.Collections.Generic;
using MediatR;
using PowerChat.Services.Users.Application.Friends.Queries.GetFriends.Models;

namespace PowerChat.Services.Users.Application.Friends.Queries.GetFriends
{
    public class GetFriendsQuery : IRequest<IList<FriendModel>>
    {
    }
}
