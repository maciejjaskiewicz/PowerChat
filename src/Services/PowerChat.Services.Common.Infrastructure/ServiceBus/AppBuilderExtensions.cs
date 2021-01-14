using System;
using Microsoft.AspNetCore.Builder;
using PowerChat.Services.Common.Application.Commands;
using PowerChat.Services.Common.Application.Events;
using RawRabbit;

namespace PowerChat.Services.Common.Infrastructure.ServiceBus
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder SubscribeToCommand<TCommand>(this IApplicationBuilder appBuilder) 
            where TCommand : ICommand
        {
            var services = appBuilder.ApplicationServices;

            var handler = services.GetService<ICommandHandler<TCommand>>();
            var bus = services.GetService<IBusClient>();

            bus.WithCommandHandler(handler);

            return appBuilder;
        }

        public static IApplicationBuilder SubscribeToEvent<TEvent>(this IApplicationBuilder appBuilder)
            where TEvent : IEvent
        {
            var services = appBuilder.ApplicationServices;

            var handler = services.GetService<IEventHandler<TEvent>>();
            var bus = services.GetService<IBusClient>();

            bus.WithEventHandler(handler);

            return appBuilder;
        }

        private static TService GetService<TService>(this IServiceProvider serviceProvider)
        {
            if (!(serviceProvider.GetService(typeof(TService)) is TService service))
                throw new NullReferenceException();

            return service;
        }
    }
}