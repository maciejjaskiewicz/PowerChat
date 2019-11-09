using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerChat.Application.Channels.Events;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Domain.Entities;

namespace PowerChat.Application.Channels.Commands.CreateChannel
{
    public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, long>
    {
        private readonly IPowerChatDbContext _dbContext;
        private readonly IMediator _mediator;

        public CreateChannelCommandHandler(IPowerChatDbContext dbContext, 
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<long> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
        {
            var entity = new Channel
            {
                Name = request.Name
            };

            _dbContext.Channels.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new ChannelCreatedEvent {UserId = entity.Id}, cancellationToken);

            return entity.Id;
        }
    }
}
