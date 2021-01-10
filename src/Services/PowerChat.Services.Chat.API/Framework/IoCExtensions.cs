using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerChat.Services.Common.Application.Services;
using PowerChat.Services.Chat.API.Services;

namespace PowerChat.Services.Chat.API.Framework
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddServiceComponents(this IServiceCollection services,
            IConfiguration configuration)
        {
            Infrastructure.IoCRegistration.Register(services, configuration);
            Application.IoCRegistration.Register(services, configuration);

            services.AddTransient<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
