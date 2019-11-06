using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Application.Channels.Queries.GetChannelsList.Models;
using PowerChat.Application.Common.Interfaces;

namespace PowerChat.Application.Channels.Queries.GetChannelsList
{
    public class GetChannelsListQueryHandler : IRequestHandler<GetChannelsListQuery, ChannelsListModel>
    {
        private readonly IPowerChatDbContext _dbContext;

        public GetChannelsListQueryHandler(IPowerChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ChannelsListModel> Handle(GetChannelsListQuery request, CancellationToken cancellationToken)
        {
            var model = new ChannelsListModel
            {
                Channels = await _dbContext.Channels.Select(x => new ChannelModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync(cancellationToken)
            };

            return model;
        }
    }
}
