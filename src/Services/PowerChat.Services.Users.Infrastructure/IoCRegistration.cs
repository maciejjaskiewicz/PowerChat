using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerChat.Services.Common.Infrastructure;
using PowerChat.Services.Users.Application.Services;
using PowerChat.Services.Users.Infrastructure.Persistence;

namespace PowerChat.Services.Users.Infrastructure
{
    public static class IoCRegistration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCommonInfrastructureServices(configuration);

            var connectionString = configuration.GetConnectionString("UsersDatabase");
            services.AddDbContext<PowerChatUsersDbContext>(config =>
            {
                config.UseSqlServer(connectionString);
            });

            services.AddScoped<IPowerChatServiceDbContext, PowerChatUsersDbContext>();
        }
    }
}
