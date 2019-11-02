using Autofac;
using Microsoft.Extensions.Configuration;

namespace PowerChat.Persistence.IoC
{
    public class PersistenceModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public PersistenceModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
