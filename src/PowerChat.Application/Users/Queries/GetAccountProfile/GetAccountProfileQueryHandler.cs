using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Users.Queries.GetAccountProfile.Models;

namespace PowerChat.Application.Users.Queries.GetAccountProfile
{
    public class GetAccountProfileQueryHandler : IRequestHandler<GetAccountProfileQuery, AccountProfileModel>
    {
        private readonly IPowerChatDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public GetAccountProfileQueryHandler(IPowerChatDbContext dbContext, 
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<AccountProfileModel> Handle(GetAccountProfileQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserIdOrThrow();
            var user = await _dbContext.Users.FindAsync(currentUserId);

            return new AccountProfileModel
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
