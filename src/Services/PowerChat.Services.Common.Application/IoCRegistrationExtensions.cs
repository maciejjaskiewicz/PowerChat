using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PowerChat.Services.Common.Application.Behaviors;

namespace PowerChat.Services.Common.Application
{
    public static class IoCRegistrationExtensions
    {
        public static IServiceCollection AddCommonApplicationServices(this IServiceCollection services)
        {
            ValidatorOptions.Global.LanguageManager.Enabled = false;
            services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());

            services.AddMediatR(Assembly.GetCallingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            return services;
        }
    }
}
