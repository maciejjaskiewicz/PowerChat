using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using PowerChat.Services.Chat.Application.Services;

namespace PowerChat.Services.Chat.Infrastructure.Services
{
    public class ConnectedUsersService : IConnectedUsersService
    {
        private readonly IDistributedCache _distributedCache;

        public ConnectedUsersService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public bool IsConnected(string userIdentityId)
        {
            return !string.IsNullOrEmpty(_distributedCache.GetString(userIdentityId));
        }

        public async Task<bool> IsConnectedAsync(string userIdentityId, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(await _distributedCache.GetStringAsync(userIdentityId, cancellationToken));
        }
    }
}
