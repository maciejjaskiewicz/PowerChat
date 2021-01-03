using System.Threading.Tasks;
using MediatR;

namespace PowerChat.Services.Common.Application.Services
{
    public interface IMessageBroker
    {
        Task PublishAsync(INotification message);
    }
}
