using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using PowerChat.Common.Results;
using PowerChat.Services.Common.Application.Results;
using PowerChat.Services.Common.Application.Services;
using PowerChat.Services.Chat.Application.Services;

namespace PowerChat.Services.Chat.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPowerChatServiceDbContext _dbContext;

        public long? UserId
        {
            get
            {
                if (!IsAuthenticated) return null;

                return _dbContext.Users
                    .SingleOrDefault(x => x.IdentityId == UserIdentityId)
                    ?.Id;
            }
        }

        public string UserIdentityId =>
            _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);
        public bool IsAuthenticated => !string.IsNullOrEmpty(UserIdentityId);

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, 
            IPowerChatServiceDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public long GetUserIdOrThrow()
        {
            if (!IsAuthenticated)
                throw new UnauthorizedAccessException("Unauthorized.");

            return UserId ?? throw new NullReferenceException();
        }

        public string GetUserIdentityIdOrThrow()
        {
            if (!IsAuthenticated)
                throw new UnauthorizedAccessException("Unauthorized.");

            return UserIdentityId;
        }

        ApplicationResult<string> ICurrentUserService.GetResultUserId()
        {
            if (!IsAuthenticated)
                return ApplicationResult<string>.Fail(PowerChatError
                    .Create("Unauthorized", "Unauthorized"));

            return ApplicationResult<string>.Ok(UserIdentityId);
        }
    }
}
