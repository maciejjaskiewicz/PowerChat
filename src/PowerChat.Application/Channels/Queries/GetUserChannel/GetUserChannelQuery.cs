using MediatR;
using PowerChat.Application.Channels.Queries.GetChannelsList.Models;

namespace PowerChat.Application.Channels.Queries.GetUserChannel
{
    public class GetUserChannelQuery : IRequest<ChannelPreviewModel>
    {
        public long UserId { get; set; }
    }
}
