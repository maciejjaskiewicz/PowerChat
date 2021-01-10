using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Common.Application.Services;
using PowerChat.Services.Users.Application.Friends.Queries.GetFriends.Models;
using PowerChat.Services.Users.Application.Services;
using PowerChat.Services.Users.Application.Users.Services;

namespace PowerChat.Services.Users.Application.Friends.Queries.GetFriends
{
    public class GetFriendsQueryHandler : IRequestHandler<GetFriendsQuery, IList<FriendModel>>
    {
        private readonly IPowerChatServiceDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IConnectedUsersService _connectedUsersService;

        public GetFriendsQueryHandler(IPowerChatServiceDbContext dbContext, 
            ICurrentUserService currentUserService, 
            IConnectedUsersService connectedUsersService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _connectedUsersService = connectedUsersService;
        }

        public async Task<IList<FriendModel>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
        {
            var currentUserIdentityId = _currentUserService.GetUserIdentityIdOrThrow();
            var currentUserId = _dbContext.Users
                .SingleAsync(x => x.IdentityId == currentUserIdentityId, cancellationToken)
                .Id;

            var requestedBy = await _dbContext.Friendships
                .Where(x => x.RequestedById == currentUserId)
                .Select(x => new FriendModel
                {
                    Id = x.RequestedTo.Id,
                    Name = x.RequestedTo.Name.FullName,
                    Gender = x.RequestedTo.Gender.ToString(),
                    Approved = x.Approved
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var requestedTo = await _dbContext.Friendships
                .Where(x => x.RequestedToId == currentUserId)
                .Select(x => new FriendModel
                {
                    Id = x.RequestedBy.Id,
                    Name = x.RequestedBy.Name.FullName,
                    Gender = x.RequestedBy.Gender.ToString(),
                    Approved = x.Approved
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var friends = requestedBy.Union(requestedTo).ToList();

            foreach (var friend in friends)
            {
                friend.IsOnline = _connectedUsersService.ConnectedUsers
                    .Any(x => x.UserId == friend.Id);
            }

            return friends;
        }
    }
}
