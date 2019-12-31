using System.Reflection;
using Autofac;
using PowerChat.API.Services;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Common.Interfaces;

namespace PowerChat.API.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.IsAssignableTo<IApiService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<ConnectedUsersService>()
                .As<IInternalConnectedUsersService>()
                .As<IConnectedUsersService>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
