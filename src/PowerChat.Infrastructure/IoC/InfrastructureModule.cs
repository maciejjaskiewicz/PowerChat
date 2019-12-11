using Autofac;
using Microsoft.Extensions.Configuration;
using PowerChat.Infrastructure.IoC.Modules;

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
            builder.RegisterModule<ServiceModule>();
        }
    }
}
