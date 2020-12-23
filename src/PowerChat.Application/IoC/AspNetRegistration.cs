using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PowerChat.Application.IoC
{
    public static class AspNetRegistration
    {
        public static void Register(IServiceCollection services)
        {
            // FluentValidation configuration
            ValidatorOptions.Global.LanguageManager.Enabled = false;
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // MediatR configuration
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
