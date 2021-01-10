using System;
using System.Reflection;
using System.Threading;
using PowerChat.Services.Common.Application.Commands;
using PowerChat.Services.Common.Application.Events;
using RawRabbit;
using RawRabbit.Configuration.Consumer;
using RawRabbit.Pipe;

namespace PowerChat.Services.Common.Infrastructure.ServiceBus
{
    public static class ServiceBusExtensions
    {
        public static IBusClient WithCommandHandler<TCommand>(this IBusClient bus,
            ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            bus.SubscribeAsync<TCommand>(msg => handler.Handle(msg, CancellationToken.None),
                ctx => ctx.Properties.Add(PipeKey.ConfigurationAction,
                    (Action<IConsumerConfigurationBuilder>)(cfg => cfg.FromDeclaredQueue(q 
                        => q.WithName(GetQueueName<TCommand>())))));

            return bus;
        }

        public static IBusClient WithEventHandler<TEvent>(this IBusClient bus,
            IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            bus.SubscribeAsync<TEvent>(msg => handler.Handle(msg, CancellationToken.None),
                ctx => ctx.Properties.Add(PipeKey.ConfigurationAction,
                    (Action<IConsumerConfigurationBuilder>)(cfg => cfg.FromDeclaredQueue(q 
                        => q.WithName(GetQueueName<TEvent>())))));

            return bus;
        }

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly()?.GetName()}/{typeof(T).Name}";
    }
}