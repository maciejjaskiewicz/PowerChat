using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Application.Channels.Queries.GetChannelsList.Models;
using PowerChat.Application.Common.Interfaces;

namespace PowerChat.Application.Channels.Queries.GetChannelsList
{
    public class GetChannelsQueryHandler : IRequestHandler<GetChannelsQuery, IList<ChannelPreviewModel>>
    {
        private readonly IPowerChatDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IConnectedUsersService _connectedUsersService;

        public GetChannelsQueryHandler(IPowerChatDbContext dbContext, 
            ICurrentUserService currentUserService, 
            IConnectedUsersService connectedUsersService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _connectedUsersService = connectedUsersService;
        }

        public async Task<IList<ChannelPreviewModel>> Handle(GetChannelsQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserIdOrThrow();

            var channels = await _dbContext.Channels
                .Where(c => c.Interlocutors.Any(i => i.UserId == currentUserId))
                .Select(c => new ChannelPreviewModel
                {
                    Id = c.Id,
                    InterlocutorUserId = c.Interlocutors.Single(i => i.UserId != currentUserId).UserId,
                    Name = c.Interlocutors.Single(i => i.UserId != currentUserId).User.Name.FullName,
                    Gender = c.Interlocutors.Single(i => i.UserId != currentUserId).User.Gender.ToString(),
                    LastMessage = c.Messages.OrderBy(m => m.CreatedDate).Last().Content,
                    LastMessageDate = c.Messages.OrderBy(m => m.CreatedDate).Last().CreatedDate,
                    Seen = c.Messages.OrderBy(m => m.CreatedDate).Last().Seen != null,
                    Own = c.Messages.OrderBy(m => m.CreatedDate).Last().SenderId == currentUserId,
                    CreatedDate = c.CreatedDate
                })
                .OrderByDescending(c => c.LastMessageDate)
                    .ThenByDescending(c => c.CreatedDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            foreach (var channel in channels)
            {
                channel.IsOnline = _connectedUsersService.ConnectedUsers
                    .Any(x => x.UserId == channel.InterlocutorUserId);
            }

            return channels;
        }
    }
}
