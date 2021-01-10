using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerChat.Common.Interfaces;
using PowerChat.Services.Common.Application.Services;
using PowerChat.Services.Common.Infrastructure.DistributedCache;
using PowerChat.Services.Common.Infrastructure.ServiceBus;
using PowerChat.Services.Common.Infrastructure.Services;

namespace PowerChat.Services.Common.Infrastructure
{
    public static class IoCRegistrationExtensions
    {
        public static IServiceCollection AddCommonInfrastructureServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddRabbitMq(configuration.GetSection("RabbitMq"));
            services.AddRedis(configuration.GetSection("Redis"));
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddTransient<IMessageBroker, MessageBroker>();

            return services;
        }
    }
}
