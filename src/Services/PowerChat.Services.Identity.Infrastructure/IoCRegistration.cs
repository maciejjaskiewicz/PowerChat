using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerChat.Services.Common.Infrastructure;

namespace PowerChat.Services.Identity.Infrastructure
{
    public static class IoCRegistration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCommonInfrastructureServices(configuration);
        }
    }
}
