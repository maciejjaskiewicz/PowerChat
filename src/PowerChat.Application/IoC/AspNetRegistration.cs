using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PowerChat.Application.IoC
{
    public static class AspNetRegistration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
