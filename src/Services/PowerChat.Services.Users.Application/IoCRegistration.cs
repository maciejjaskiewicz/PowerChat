using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerChat.Services.Common.Application;
using PowerChat.Services.Common.Application.Contract.Identity;
using PowerChat.Services.Common.Application.Events;
using PowerChat.Services.Users.Application.Users.Events;

namespace PowerChat.Services.Users.Application
{
    public static class IoCRegistration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCommonApplicationServices();

            services.AddTransient<IEventHandler<AccountCreatedEvent>, AccountCreatedEventHandler>();
        }
    }
}