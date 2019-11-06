using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Common;

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
            builder.Register(c =>
            {
                var connectionString = _configuration.GetConnectionString("PowerChatDatabase");

                var options = new DbContextOptionsBuilder();
                options.UseSqlServer(connectionString);

                return new PowerChatDbContext(options.Options, c.Resolve<IDateTime>());
            })
            .AsSelf()
            .As<IPowerChatDbContext>()
            .InstancePerLifetimeScope();
        }
    }
}
