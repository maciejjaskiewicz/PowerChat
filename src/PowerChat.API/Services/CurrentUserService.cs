using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Common.Results;
using PowerChat.Common.Results;

namespace PowerChat.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public long? UserId { get; }
        public bool IsAuthenticated { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var userIdString = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString) == false)
            {
                var parsed = long.TryParse(userIdString, out var userId);
                UserId = parsed ? userId : (long?)null;
            }

            IsAuthenticated = UserId != null;
        }

        public long GetUserIdOrThrow()
        {
            if (UserId == null || IsAuthenticated == false)
            {
                throw new UnauthorizedAccessException("Unauthorized.");
            }

            return UserId.Value;
        }

        public ApplicationResult<long> GetResultUserId()
        {
            if (UserId == null || IsAuthenticated == false)
            {
                return ApplicationResult<long>.Fail(PowerChatError.Create("Unauthorized", "Unauthorized"));
            }

            return ApplicationResult<long>.Ok(UserId.Value);
        }
    }
}
