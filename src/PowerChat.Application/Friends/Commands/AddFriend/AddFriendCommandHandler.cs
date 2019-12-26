using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Common.Results;
using PowerChat.Application.Friends.Events;
using PowerChat.Domain.Entities;

namespace PowerChat.Application.Friends.Commands.AddFriend
{
    public class AddFriendCommandHandler : IRequestHandler<AddFriendCommand, ApplicationResult>
    {
        private readonly IPowerChatDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public AddFriendCommandHandler(IPowerChatDbContext dbContext, 
            ICurrentUserService currentUserService, 
            IMediator mediator)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task<ApplicationResult> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            var currentUserResult = _currentUserService.GetResultUserId();

            if (currentUserResult.Succeeded == false)
            {
                return ApplicationResult.Fail(currentUserResult.Error);
            }

            var requestedBy = await _dbContext.Users.FindAsync(new object[] { currentUserResult.Value }, cancellationToken);
            var requestedTo = await _dbContext.Users.FindAsync(new object[] { request.UserId }, cancellationToken);

            if (await FriendshipExistsAsync(requestedBy.Id, requestedTo.Id, cancellationToken))
            {
                return ApplicationResult.Ok();
            }

            var friendship = new Friendship
            {
                RequestedBy = requestedBy,
                RequestedTo = requestedTo,
                Approved = true // TODO
            };

            await _dbContext.Friendships.AddAsync(friendship, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new FriendshipCreatedEvent
            {
                FriendshipId = friendship.Id,
                RequestedById = requestedBy.Id,
                RequestedToId = requestedTo.Id
            }, cancellationToken);

            return ApplicationResult.Ok();
        }

        public async Task<bool> FriendshipExistsAsync(long userId1, long userId2, CancellationToken cancellationToken)
        {
            var friendshipExists = await _dbContext.Friendships
                .AnyAsync(x => (x.RequestedById == userId1 && x.RequestedToId == userId2) ||
                               (x.RequestedToId == userId1 && x.RequestedById == userId2),
                    cancellationToken);

            return friendshipExists;
        }
    }
}
