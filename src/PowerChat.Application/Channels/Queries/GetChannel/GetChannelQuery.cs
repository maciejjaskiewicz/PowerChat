using MediatR;
using PowerChat.Application.Channels.Models;

namespace PowerChat.Application.Channels.Queries.GetChannel
{
    public class GetChannelQuery : IRequest<ChannelModel>
    {
        public long Id { get; set; }
    }
}
