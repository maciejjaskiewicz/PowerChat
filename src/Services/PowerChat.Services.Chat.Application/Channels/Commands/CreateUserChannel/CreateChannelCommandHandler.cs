using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerChat.Services.Chat.Application.Channels.Events;
using PowerChat.Services.Chat.Application.Services;
using PowerChat.Services.Chat.Core.Entities;
using PowerChat.Services.Common.Application.Results;
using PowerChat.Services.Common.Application.Services;

namespace PowerChat.Services.Chat.Application.Channels.Commands.CreateUserChannel
{
    public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, ApplicationResult<long>>
    {
        private readonly IPowerChatServiceDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public CreateChannelCommandHandler(IPowerChatServiceDbContext dbContext, 
            IMediator mediator, 
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public async Task<ApplicationResult<long>> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
        {
            var currentUserResult = _currentUserService.GetResultUserId();
            if (!currentUserResult.Succeeded)
            {
                return ApplicationResult<long>.Fail(currentUserResult.Error);
            }

            var user = await _dbContext.Users.FindAsync(new object[] {request.UserId}, cancellationToken);
            var currentUser = await _dbContext.Users.FindAsync(new object[] {currentUserResult.Value}, cancellationToken);

            var channel = new Channel();
            var userInterlocutor = new Interlocutor
            {
                Channel = channel,
                User = user
            };
            var currentUserInterlocutor = new Interlocutor
            {
                Channel = channel,
                User = currentUser
            };

            await _dbContext.Channels.AddAsync(channel, cancellationToken);
            await _dbContext.Interlocutors.AddAsync(userInterlocutor, cancellationToken);
            await _dbContext.Interlocutors.AddAsync(currentUserInterlocutor, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new ChannelCreatedEvent { ChannelId = channel.Id }, cancellationToken);

            return ApplicationResult<long>.Ok(channel.Id);
        }
    }
}
