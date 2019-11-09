using MediatR;

namespace PowerChat.Application.Channels.Commands.CreateChannel
{
    public class CreateChannelCommand : IRequest<long>
    {
        public string Name { get; set; }
    }
}
