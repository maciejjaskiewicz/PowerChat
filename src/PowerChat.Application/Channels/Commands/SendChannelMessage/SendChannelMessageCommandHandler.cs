using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Application.Channels.Models;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Common.Results;
using PowerChat.Domain.Entities;

namespace PowerChat.Application.Channels.Commands.SendChannelMessage
{
    public class SendChannelMessageCommandHandler : IRequestHandler<SendChannelMessageCommand, ApplicationResult<long>>
    {
        private readonly IPowerChatDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IConnectedUsersService _connectedUsersService;

        public SendChannelMessageCommandHandler(IPowerChatDbContext dbContext,
            ICurrentUserService currentUserService, 
            IConnectedUsersService connectedUsersService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _connectedUsersService = connectedUsersService;
        }

        public async Task<ApplicationResult<long>> Handle(SendChannelMessageCommand request, CancellationToken cancellationToken)
        {
            var currentUserResult = _currentUserService.GetResultUserId();
            if (!currentUserResult.Succeeded)
            {
                return ApplicationResult<long>.Fail(currentUserResult.Error);
            }

            var channel = await _dbContext.Channels.FindAsync(new object[] {request.ChannelId}, cancellationToken);
            var sender = await _dbContext.Users.FindAsync(new object[] {currentUserResult.Value}, cancellationToken);
            var interlocutorUserId = await _dbContext.Interlocutors
                .Where(i => i.ChannelId == channel.Id)
                .Where(i => i.UserId != currentUserResult.Value)
                .Select(i => i.UserId)
                .SingleAsync(cancellationToken);

            var message = new Message
            {
                Channel = channel,
                Sender = sender,
                Content = request.Content
            };

            await _dbContext.Messages.AddAsync(message, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _connectedUsersService.SendAsync(interlocutorUserId, "ReceiveMessage", new
            {
                message = new MessageModel
                {
                    Id = message.Id,
                    AuthorId = sender.Id,
                    Content = message.Content,
                    Own = false,
                    Seen = message.Seen,
                    SentDate = message.CreatedDate
                },
                channelId = channel.Id
            });

            return ApplicationResult<long>.Ok(message.Id);
        }
    }
}
