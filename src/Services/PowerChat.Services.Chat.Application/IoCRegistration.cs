using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerChat.Services.Chat.Application.Services;
using PowerChat.Services.Chat.Application.Users.Events;
using PowerChat.Services.Common.Application;
using PowerChat.Services.Common.Application.Contract.Identity;
using PowerChat.Services.Common.Application.Events;

namespace PowerChat.Services.Chat.Application
{
    public static class IoCRegistration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCommonApplicationServices();

            services.AddTransient<IEventHandler<AccountCreatedEvent>, AccountCreatedEventHandler>();

            services.AddTransient<IUserService, UserService>();
        }
    }
}