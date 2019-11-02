using Autofac;
using Microsoft.Extensions.Configuration;
using PowerChat.Common;

namespace PowerChat.Infrastructure.IoC
{
    public class InfrastructureModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public InfrastructureModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MachineDateTime>()
                .As<IDateTime>()
                .InstancePerLifetimeScope();
        }
    }
}
