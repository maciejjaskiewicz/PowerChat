using MediatR;
using PowerChat.Services.Common.Application.Results;

namespace PowerChat.Services.Chat.Application.Channels.Commands.CreateUserChannel
{
    public class CreateChannelCommand : IRequest<ApplicationResult<long>>
    {
        public long UserId { get; set; }
    }
}
