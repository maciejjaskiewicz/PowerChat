﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerChat.Services.Common.Application;
using PowerChat.Services.Common.Application.Commands;
using PowerChat.Services.Common.Application.Contract.Identity;
using PowerChat.Services.Common.Application.Contract.Users;
using PowerChat.Services.Common.Application.Events;
using PowerChat.Services.Users.Application.Users.Commands;
using PowerChat.Services.Users.Application.Users.Events;
using PowerChat.Services.Users.Application.Users.Services;

namespace PowerChat.Services.Users.Application
{
    public static class IoCRegistration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCommonApplicationServices();

            services.AddTransient<IEventHandler<AccountCreatedEvent>, AccountCreatedEventHandler>();
            services.AddTransient<ICommandHandler<SendUserMsgNotificationCommand>, SendUserMsgNotificationCommandHandler>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserActivityService, UserActivityService>();
        }
    }
}