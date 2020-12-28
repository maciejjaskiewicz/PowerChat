using Microsoft.EntityFrameworkCore;

namespace PowerChat.Persistence
{
    public class PowerChatIdentityDbContextFactory : DesignTimeDbContextFactoryBase<PowerChatDbContext>
    {
        protected override string ConnectionStringName { get; } = "IdentityDatabase";
        protected override string Project { get; } = "PowerChat.IdentityServer";

        protected override PowerChatDbContext CreateNewInstance(DbContextOptions<PowerChatDbContext> options)
        {
            return new PowerChatDbContext(options);
        }
    }
}
