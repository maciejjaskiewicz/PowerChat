using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Chat.Application.Channels.Models;
using PowerChat.Services.Chat.Application.Services;
using PowerChat.Services.Chat.Core.Entities;
using PowerChat.Services.Common.Application.Contract.Users;
using PowerChat.Services.Common.Application.Results;
using PowerChat.Services.Common.Application.Services;

namespace PowerChat.Services.Chat.Application.Channels.Commands.SendChannelMessage
{
    public class SendChannelMessageCommandHandler : IRequestHandler<SendChannelMessageCommand, ApplicationResult<long>>
    {
        private readonly IPowerChatServiceDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMessageBroker _messageBroker;

        public SendChannelMessageCommandHandler(IPowerChatServiceDbContext dbContext,
            ICurrentUserService currentUserService,
            IMessageBroker messageBroker)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _messageBroker = messageBroker;
        }

        public async Task<ApplicationResult<long>> Handle(SendChannelMessageCommand request, CancellationToken cancellationToken)
        {
            var currentUserResult = _currentUserService.GetResultUserId();
            if (!currentUserResult.Succeeded)
            {
                return ApplicationResult<long>.Fail(currentUserResult.Error);
            }

            var channel = await _dbContext.Channels.FindAsync(new object[] { request.ChannelId }, cancellationToken);
            var sender = await _dbContext.Users.FindAsync(new object[] { currentUserResult.Value }, cancellationToken);

            var interlocutorUserIdentityId = await _dbContext.Interlocutors
                .Where(i => i.ChannelId == channel.Id)
                .Where(i => i.UserId != _currentUserService.UserId.Value)
                .Select(i => i.User.IdentityId)
                .SingleAsync(cancellationToken);

            var message = new Message
            {
                Channel = channel,
                Sender = sender,
                Content = request.Content
            };

            await _dbContext.Messages.AddAsync(message, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var sendNotificationCommand = new SendUserMsgNotificationCommand
            {
                UserIdentityId = interlocutorUserIdentityId,
                MsgNotification = new MsgNotificationModel
                {
                    Id = message.Id,
                    AuthorId = sender.Id,
                    Content = message.Content,
                    Own = false,
                    Seen = message.Seen,
                    SentDate = message.CreatedDate
                },
                ChannelId = channel.Id
            };

            await _messageBroker.PublishAsync(sendNotificationCommand);

            return ApplicationResult<long>.Ok(message.Id);
        }
    }
}
