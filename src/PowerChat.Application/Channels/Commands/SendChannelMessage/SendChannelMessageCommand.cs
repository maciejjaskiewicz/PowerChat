using MediatR;
using PowerChat.Application.Common.Results;

namespace PowerChat.Application.Channels.Commands.SendChannelMessage
{
    public class SendChannelMessageCommand : IRequest<ApplicationResult<long>>
    {
        public long ChannelId { get; set; }
        public string Content { get; set; }
    }
}
