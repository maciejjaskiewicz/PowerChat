using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerChat.Services.Common.Infrastructure;
using PowerChat.Services.Chat.Application.Services;
using PowerChat.Services.Chat.Infrastructure.Persistence;
using PowerChat.Services.Chat.Infrastructure.Services;

namespace PowerChat.Services.Chat.Infrastructure
{
    public static class IoCRegistration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCommonInfrastructureServices(configuration);

            var connectionString = configuration.GetConnectionString("ChatDatabase");
            services.AddDbContext<PowerChatDbContext>(config =>
            {
                config.UseSqlServer(connectionString);
            });

            services.AddScoped<IPowerChatServiceDbContext, PowerChatDbContext>();

            services.AddTransient<IConnectedUsersService, ConnectedUsersService>();
        }
    }
}
