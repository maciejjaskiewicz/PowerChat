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
        private readonly IConnectedUsersService _connectedUsersService;

        public GetUserQueryHandler(IPowerChatDbContext dbContext, 
            ICurrentUserService currentUserService, 
            IConnectedUsersService connectedUsersService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _connectedUsersService = connectedUsersService;
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
                               x.SentFriendshipRequests.Any(f => f.RequestedToId == currentUserId),
                    LastActive = x.LastActive
                    
                })
                .AsNoTracking()
                .SingleAsync(cancellationToken);

            user.IsOnline = _connectedUsersService.ConnectedUsers
                .Any(x => x.UserId == user.Id);

            return user;
        }
    }
}
