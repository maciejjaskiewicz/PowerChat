using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using PowerChat.Common.Exceptions;
using PowerChat.Services.Users.API.Hubs;
using PowerChat.Services.Users.Application.Users.Services;

namespace PowerChat.Services.Users.API.Services
{
    public interface IInternalConnectedUsersService : IConnectedUsersService
    {
        string AddUser(string userIdentityId, string connectionId);
        string RemoveUser(string userIdentityId);
    }

    public class ConnectedUsersService : IInternalConnectedUsersService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IDistributedCache _distributedCache;

        public ConnectedUsersService(IHubContext<ChatHub> hubContext, 
            IDistributedCache distributedCache)
        {
            _hubContext = hubContext;
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

        //TODO
        public async Task SendAsync(string userIdentityId, string method, object payload)
        {
            if (IsConnected(userIdentityId))
            {
                await _hubContext.Clients.User(userIdentityId)
                    .SendAsync(method, payload);
            }
        }

        public string AddUser(string userIdentityId, string connectionId)
        {
            if (string.IsNullOrEmpty(userIdentityId) == false)
            {
                if (!IsConnected(userIdentityId))
                    _distributedCache.SetString(userIdentityId, connectionId);

                return userIdentityId;
            }

            throw new PowerChatException("UserIdParseFailed",
                $"Failed to parse userId: {userIdentityId}");
        }

        public string RemoveUser(string userIdentityId)
        {
            if (!string.IsNullOrEmpty(userIdentityId) && IsConnected(userIdentityId))
            {
                _distributedCache.Remove(userIdentityId);
            }

            return null;
        }
    }
}
