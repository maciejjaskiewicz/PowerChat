using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Chat.Application.Channels.Commands.CreateUserChannel;
using PowerChat.Services.Chat.Application.Channels.Queries.GetChannelsList.Models;
using PowerChat.Services.Chat.Application.Services;
using PowerChat.Services.Chat.Core.Entities;
using PowerChat.Services.Common.Application.Services;

namespace PowerChat.Services.Chat.Application.Channels.Queries.GetUserChannel
{
    public class GetUserChannelQueryHandler : IRequestHandler<GetUserChannelQuery, ChannelPreviewModel>
    {
        private readonly IPowerChatServiceDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        private readonly IConnectedUsersService _connectedUsersService;

        public GetUserChannelQueryHandler(IPowerChatServiceDbContext dbContext, 
            IMediator mediator, 
            ICurrentUserService currentUserService, 
            IConnectedUsersService connectedUsersService)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _currentUserService = currentUserService;
            _connectedUsersService = connectedUsersService;
        }

        public async Task<ChannelPreviewModel> Handle(GetUserChannelQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserIdOrThrow();

            if (!await UserChannelExists(request.UserId, currentUserId, cancellationToken))
            {
                var createChannelResult = await _mediator.Send(new CreateChannelCommand
                {
                    UserId = request.UserId
                }, cancellationToken);

                createChannelResult.ThrowIfFailed();
            }

            var channel = await UserChannelQuery(request.UserId, currentUserId)
                .Select(c => new ChannelPreviewModel
                {
                    Id = c.Id,
                    InterlocutorUserId = c.Interlocutors.Single(i => i.UserId != currentUserId).UserId,
                    InterlocutorUserIdentityId = c.Interlocutors.Single(i => i.UserId != currentUserId).User.IdentityId,
                    Name = c.Interlocutors.Single(i => i.UserId != currentUserId).User.Name.FullName,
                    Gender = c.Interlocutors.Single(i => i.UserId != currentUserId).User.Gender.ToString(),
                    LastMessage = c.Messages.OrderBy(m => m.CreatedDate).Last().Content,
                    LastMessageDate = c.Messages.OrderBy(m => m.CreatedDate).Last().CreatedDate,
                    Seen = c.Messages.OrderBy(m => m.CreatedDate).Last().Seen != null,
                    CreatedDate = c.CreatedDate
                })
                .AsNoTracking()
                .SingleAsync(cancellationToken);

            channel.IsOnline = await _connectedUsersService.IsConnectedAsync(
                channel.InterlocutorUserIdentityId, cancellationToken);

            return channel;
        }

        private async Task<bool> UserChannelExists(long userId, long currentUserId, CancellationToken cancellationToken)
        {
            return await UserChannelQuery(userId, currentUserId)
                .AsNoTracking()
                .AnyAsync(cancellationToken);
        }

        private IQueryable<Channel> UserChannelQuery(long userId, long currentUserId)
        {
            return _dbContext.Channels
                .Where(c => c.Interlocutors.Count() == 2)
                .Where(c => c.Interlocutors.Any(i => i.UserId == userId))
                .Where(c => c.Interlocutors.Any(i => i.UserId == currentUserId));
        }
    }
}
