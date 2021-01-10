using MediatR;
using PowerChat.Services.Common.Application.Results;

namespace PowerChat.Services.Chat.Application.Channels.Commands.SendChannelMessage
{
    public class SendChannelMessageCommand : IRequest<ApplicationResult<long>>
    {
        public long ChannelId { get; set; }
        public string Content { get; set; }
    }
}
