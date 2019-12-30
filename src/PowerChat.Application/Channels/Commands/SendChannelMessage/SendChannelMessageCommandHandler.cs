using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Common.Results;
using PowerChat.Domain.Entities;

namespace PowerChat.Application.Channels.Commands.SendChannelMessage
{
    public class SendChannelMessageCommandHandler : IRequestHandler<SendChannelMessageCommand, ApplicationResult<long>>
    {
        private readonly IPowerChatDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public SendChannelMessageCommandHandler(IPowerChatDbContext dbContext,
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<ApplicationResult<long>> Handle(SendChannelMessageCommand request, CancellationToken cancellationToken)
        {
            var currentUserResult = _currentUserService.GetResultUserId();
            if (!currentUserResult.Succeeded)
            {
                return ApplicationResult<long>.Fail(currentUserResult.Error);
            }

            var message = new Message
            {
                Channel = await _dbContext.Channels.FindAsync(new object[] {request.ChannelId}, cancellationToken),
                Sender = await _dbContext.Users.FindAsync(new object[] {currentUserResult.Value}, cancellationToken),
                Content = request.Content
            };

            await _dbContext.Messages.AddAsync(message, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return ApplicationResult<long>.Ok(message.Id);
        }
    }
}
