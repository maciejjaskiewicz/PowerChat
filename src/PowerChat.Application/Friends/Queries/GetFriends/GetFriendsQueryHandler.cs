using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Friends.Queries.GetFriends.Models;

namespace PowerChat.Application.Friends.Queries.GetFriends
{
    public class GetFriendsQueryHandler : IRequestHandler<GetFriendsQuery, IList<FriendModel>>
    {
        private readonly IPowerChatDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public GetFriendsQueryHandler(IPowerChatDbContext dbContext, 
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<IList<FriendModel>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserIdOrThrow();

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

            return friends;
        }
    }
}
