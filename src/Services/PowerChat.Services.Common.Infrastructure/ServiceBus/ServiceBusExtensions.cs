using System.Threading;
using PowerChat.Services.Common.Application.Commands;
using PowerChat.Services.Common.Application.Events;
using RawRabbit;

namespace PowerChat.Services.Common.Infrastructure.ServiceBus
{
    public static class ServiceBusExtensions
    {
        public const string CommandQueueName = "powerchat-service-bus-commands";
        public const string EventQueueName = "powerchat-service-bus-events";

        public static IBusClient WithCommandHandler<TCommand>(this IBusClient bus,
            ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            bus.SubscribeAsync<TCommand>(async (msg, context) =>
            {
                await handler.Handle(msg, CancellationToken.None);
            }, cfg =>
            {
                cfg.WithQueue(q => q.WithName(CommandQueueName));
                cfg.WithSubscriberId(string.Empty);
            });

            return bus;
        }

        public static IBusClient WithEventHandler<TEvent>(this IBusClient bus,
            IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            bus.SubscribeAsync<TEvent>(async (msg, context) =>
            {
                await handler.Handle(msg, CancellationToken.None);
            }, cfg =>
            {
                cfg.WithQueue(q => q.WithName(EventQueueName));
                cfg.WithSubscriberId(string.Empty);
            });

            return bus;
        }
    }
}