using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PowerChat.API.Services;

namespace PowerChat.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IInternalConnectedUsersService _connectedUsersService;

        public ChatHub(ILogger<ChatHub> logger, 
            IInternalConnectedUsersService connectedUsersService)
        {
            _logger = logger;
            _connectedUsersService = connectedUsersService;
        }

        public override async Task OnConnectedAsync()
        {
            var userIdString = Context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var connectionId = Context?.ConnectionId;

            _connectedUsersService.AddUser(userIdString, connectionId);

            _logger.LogInformation($"[SignalR] New Connection: {connectionId}, userId: {userIdString}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userIdString = Context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var connectionId = Context?.ConnectionId;

            _connectedUsersService.RemoveUser(userIdString);

            _logger.LogInformation($"[SignalR] Disconnected: {connectionId}, userId: {userIdString}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
