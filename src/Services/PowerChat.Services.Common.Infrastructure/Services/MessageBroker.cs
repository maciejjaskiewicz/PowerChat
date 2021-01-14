using System.Threading.Tasks;
using MediatR;
using PowerChat.Services.Common.Application.Services;
using RawRabbit;

namespace PowerChat.Services.Common.Infrastructure.Services
{
    public class MessageBroker : IMessageBroker
    {
        private readonly IBusClient _busClient;

        public MessageBroker(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task PublishAsync(INotification message)
        {
            await _busClient.PublishAsync(message);
        }
    }
}
