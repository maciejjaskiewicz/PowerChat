using System.Threading;
using System.Threading.Tasks;
using PowerChat.Services.Common.Application.Commands;
using PowerChat.Services.Common.Application.Contract.Users;
using PowerChat.Services.Users.Application.Users.Services;

namespace PowerChat.Services.Users.Application.Users.Commands
{
    public class SendUserMsgNotificationCommandHandler : ICommandHandler<SendUserMsgNotificationCommand>
    {
        private readonly IConnectedUsersService _connectedUsersService;

        public SendUserMsgNotificationCommandHandler(IConnectedUsersService connectedUsersService)
        {
            _connectedUsersService = connectedUsersService;
        }

        public async Task Handle(SendUserMsgNotificationCommand msgNotification, CancellationToken cancellationToken)
        {
            var payload = new
            {
                message = msgNotification.MsgNotification,
                channelId = msgNotification.ChannelId
            };

            await _connectedUsersService.SendAsync(msgNotification.UserIdentityId, "ReceiveMessage",
                payload);
        }
    }
}
