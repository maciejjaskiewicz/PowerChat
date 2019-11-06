using Autofac;
using PowerChat.Application.IoC.Modules;

namespace PowerChat.Application.IoC
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<MediatRModule>();
        }
    }
}
