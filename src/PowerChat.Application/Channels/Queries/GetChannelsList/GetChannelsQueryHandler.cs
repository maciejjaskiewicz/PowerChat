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

        public GetChannelsQueryHandler(IPowerChatDbContext dbContext, 
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<IList<ChannelPreviewModel>> Handle(GetChannelsQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserIdOrThrow();

            var channels = await _dbContext.Channels
                .Where(c => c.Interlocutors.Any(i => i.UserId == currentUserId))
                .Select(c => new ChannelPreviewModel
                {
                    Id = c.Id,
                    Name = c.Interlocutors.Single(i => i.UserId != currentUserId).User.Name.FullName,
                    Gender = c.Interlocutors.Single(i => i.UserId != currentUserId).User.Gender.ToString(),
                    LastMessage = c.Messages.OrderBy(m => m.CreatedDate).Last().Content,
                    LastMessageDate = c.Messages.OrderBy(m => m.CreatedDate).Last().CreatedDate,
                    Seen = c.Messages.OrderBy(m => m.CreatedDate).Last().Seen != null,
                    CreatedDate = c.CreatedDate
                })
                .OrderByDescending(c => c.LastMessageDate)
                    .ThenByDescending(c => c.CreatedDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return channels;
        }
    }
}
