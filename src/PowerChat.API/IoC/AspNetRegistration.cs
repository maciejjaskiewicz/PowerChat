using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PowerChat.API.IoC
{
    public static class AspNetRegistration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            Infrastructure.IoC.AspNetRegistration.Register(services, configuration);
            Persistence.IoC.AspNetRegistration.Register(services);
            Application.IoC.AspNetRegistration.Register(services);
        }
    }
}
