using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerChat.Application.Channels.Events;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Common.Results;
using PowerChat.Domain.Entities;

namespace PowerChat.Application.Channels.Commands.CreateUserChannel
{
    public class CreateChannelCommandHandler : IRequestHandler<CreateUserChannelCommand, ApplicationResult<long>>
    {
        private readonly IPowerChatDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public CreateChannelCommandHandler(IPowerChatDbContext dbContext, 
            IMediator mediator, 
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public async Task<ApplicationResult<long>> Handle(CreateUserChannelCommand request, CancellationToken cancellationToken)
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
            await _mediator.Publish(new ChannelCreatedEvent {ChannelId = channel.Id}, cancellationToken);

            return ApplicationResult<long>.Ok(channel.Id);
        }
    }
}
