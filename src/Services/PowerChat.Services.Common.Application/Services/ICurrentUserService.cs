using PowerChat.Services.Common.Application.Results;

namespace PowerChat.Services.Common.Application.Services
{
    public interface ICurrentUserService
    {
        long? UserId { get; }
        string UserIdentityId { get; }
        bool IsAuthenticated { get; }
        long GetUserIdOrThrow();
        string GetUserIdentityIdOrThrow();
        ApplicationResult<long> GetResultUserId();
    }
}
