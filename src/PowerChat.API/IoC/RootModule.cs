using Autofac;
using Microsoft.Extensions.Configuration;
using PowerChat.API.IoC.Modules;
using PowerChat.Application.IoC;
using PowerChat.Infrastructure.IoC;
using PowerChat.Persistence.IoC;

namespace PowerChat.API.IoC
{
    public class RootModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public RootModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new InfrastructureModule(_configuration));
            builder.RegisterModule(new PersistenceModule(_configuration));
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<ServiceModule>();
        }
    }
}
