using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerChat.Services.Common.Application.Services;
using PowerChat.Services.Users.API.Services;
using PowerChat.Services.Users.Application.Users.Services;

namespace PowerChat.Services.Users.API.Framework
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddServiceComponents(this IServiceCollection services,
            IConfiguration configuration)
        {
            Infrastructure.IoCRegistration.Register(services, configuration);
            Application.IoCRegistration.Register(services, configuration);

            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IConnectedUsersService, ConnectedUsersService>();
            services.AddTransient<IInternalConnectedUsersService, ConnectedUsersService>();

            return services;
        }
    }
}
