using PowerChat.Application.Common.Results;
using PowerChat.Common.Interfaces;

namespace PowerChat.Application.Common.Interfaces
{
    public interface ICurrentUserService : IApiService
    {
        long? UserId { get; }
        bool IsAuthenticated { get; }

        long GetUserIdOrThrow();
        ApplicationResult<long> GetResultUserId();
    }
}
