using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Users.Queries.SearchUsers.Models;

namespace PowerChat.Application.Users.Queries.SearchUsers
{
    public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, IList<UserPreviewModel>>
    {
        private readonly IPowerChatDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public SearchUsersQueryHandler(IPowerChatDbContext dbContext, 
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<IList<UserPreviewModel>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserIdOrThrow();

            var usersQuery = _dbContext.Users
                .Where(x => x.Name.FirstName.Contains(request.SearchStr) ||
                            x.Name.LastName.Contains(request.SearchStr))
                .Where(x => x.Id != currentUserId);

            if (request.ExcludeFriends)
            {
                usersQuery = usersQuery
                    .Where(x => x.ReceivedFriendshipsRequests.All(f => f.RequestedById != currentUserId))
                    .Where(x => x.SentFriendshipRequests.All(f => f.RequestedToId != currentUserId));
            }

            var users = await usersQuery
                .Take(20)
                .Select(x => new UserPreviewModel
                {
                    Id = x.Id,
                    Name = x.Name.FullName,
                    Gender = x.Gender.ToString(),
                    IsFriend = false // TODO 
                })
                .AsNoTracking() // https://stackoverflow.com/questions/41577376/how-to-read-values-from-the-querystring-with-asp-net-core
                .ToListAsync(cancellationToken);

            return users;
        }
    }
}
