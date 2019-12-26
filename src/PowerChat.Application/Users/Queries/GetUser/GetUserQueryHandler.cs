using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Users.Queries.GetUser.Models;

namespace PowerChat.Application.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserModel>
    {
        private readonly IPowerChatDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public GetUserQueryHandler(IPowerChatDbContext dbContext, 
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<UserModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserIdOrThrow();

            var user = await _dbContext.Users
                .Where(x => x.Id == request.Id)
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    Firstname = x.Name.FirstName,
                    Lastname = x.Name.LastName,
                    FullName = x.Name.FullName,
                    Gender = x.Gender.ToString(),
                    About = x.About,
                    Friends = x.ReceivedFriendshipsRequests.Count() + x.SentFriendshipRequests.Count(),
                    IsFriend = x.ReceivedFriendshipsRequests.Any(f => f.RequestedById == currentUserId) ||
                               x.SentFriendshipRequests.Any(f => f.RequestedToId == currentUserId)
                })
                .AsNoTracking()
                .SingleAsync(cancellationToken);

            return user;
        }
    }
}
