using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Common.Application.Services;
using PowerChat.Services.Users.Application.Services;
using PowerChat.Services.Users.Application.Users.Queries.GetProfile.Models;

namespace PowerChat.Services.Users.Application.Users.Queries.GetProfile
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, UserProfileModel>
    {
        private readonly IPowerChatServiceDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public GetProfileQueryHandler(IPowerChatServiceDbContext dbContext, 
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<UserProfileModel> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var currentUserIdentityId = _currentUserService.GetUserIdentityIdOrThrow();
            var user = await _dbContext.Users.SingleAsync(x => x.IdentityId == currentUserIdentityId, cancellationToken);

            return new UserProfileModel
            {
                Firstname = user.Name.FirstName,
                Lastname = user.Name.LastName,
                Email = user.Email,
                Gender = user.Gender?.ToString(),
                About = user.About,
                CreatedDate = user.CreatedDate
            };
        }
    }
}