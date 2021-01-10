using MediatR;
using PowerChat.Services.Chat.Application.Channels.Models;

namespace PowerChat.Services.Chat.Application.Channels.Queries.GetChannel
{
    public class GetChannelQuery : IRequest<ChannelModel>
    {
        public long Id { get; set; }
    }
}
