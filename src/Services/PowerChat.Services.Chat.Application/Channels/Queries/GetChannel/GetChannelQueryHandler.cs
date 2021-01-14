using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Chat.Application.Channels.Errors;
using PowerChat.Services.Chat.Application.Channels.Models;
using PowerChat.Services.Chat.Application.Services;
using PowerChat.Services.Common.Application.Exceptions;
using PowerChat.Services.Common.Application.Services;

namespace PowerChat.Services.Chat.Application.Channels.Queries.GetChannel
{
    public class GetChannelQueryHandler : IRequestHandler<GetChannelQuery, ChannelModel>
    {
        private readonly IPowerChatServiceDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IConnectedUsersService _connectedUsersService;

        public GetChannelQueryHandler(IPowerChatServiceDbContext dbContext, 
            ICurrentUserService currentUserService, 
            IConnectedUsersService connectedUsersService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _connectedUsersService = connectedUsersService;
        }

        public async Task<ChannelModel> Handle(GetChannelQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserIdOrThrow();

            if (!await HasAccess(request.Id, currentUserId, cancellationToken))
            {
                throw new PowerChatApplicationException(ChannelErrorCodes.NoAccess, 
                    "You don't have access to this channel.'");
            }

            var channel = await _dbContext.Channels
                .Where(c => c.Id == request.Id)
                .Select(x => new ChannelModel
                {
                    Id = x.Id,
                    Name = x.Interlocutors.Single(i => i.UserId != currentUserId).User.Name.FullName,
                    Interlocutor = x.Interlocutors.Select(i => new InterlocutorModel
                    {
                        Id = i.User.Id,
                        IdentityId = i.User.IdentityId,
                        Name = i.User.Name.FullName,
                        Gender = i.User.Gender.ToString()
                    }).Single(i => i.Id != currentUserId),
                    Messages = x.Messages
                        .OrderBy(m => m.CreatedDate)
                        .Select(m => new MessageModel
                        {
                            Id = m.Id,
                            AuthorId = m.SenderId,
                            Content = m.Content,
                            SentDate = m.CreatedDate,
                            Seen = m.Seen,
                            Own = m.SenderId == currentUserId
                        }),
                    LastActive = x.Interlocutors.Single(i => i.UserId != currentUserId).User.LastActive
                })
                .AsNoTracking()
                .SingleAsync(cancellationToken);

            channel.IsOnline = await _connectedUsersService.IsConnectedAsync(channel.Interlocutor.IdentityId, cancellationToken);

            return channel;
        }

        private async Task<bool> HasAccess(long channelId, long userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Channels
                .Where(c => c.Id == channelId)
                .Where(c => c.Interlocutors.Any(i => i.UserId == userId))
                .AnyAsync(cancellationToken);
        }
    }
}
