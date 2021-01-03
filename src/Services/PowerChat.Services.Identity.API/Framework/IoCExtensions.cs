using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PowerChat.Services.Identity.API.Framework
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddServiceComponents(this IServiceCollection services,
            IConfiguration configuration)
        {
            Infrastructure.IoCRegistration.Register(services, configuration);
            Application.IoCRegistration.Register(services, configuration);

            return services;
        }
    }
}
