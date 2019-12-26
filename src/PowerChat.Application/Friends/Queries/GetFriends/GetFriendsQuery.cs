using System.Collections.Generic;
using MediatR;
using PowerChat.Application.Friends.Queries.GetFriends.Models;

namespace PowerChat.Application.Friends.Queries.GetFriends
{
    public class GetFriendsQuery : IRequest<IList<FriendModel>>
    {
    }
}
