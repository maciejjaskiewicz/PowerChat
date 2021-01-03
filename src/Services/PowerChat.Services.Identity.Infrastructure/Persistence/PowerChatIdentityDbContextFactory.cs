using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Common.Infrastructure.Persistence;

namespace PowerChat.Services.Identity.Infrastructure.Persistence
{
    public class PowerChatIdentityDbContextFactory : DesignTimeDbContextFactoryBase<PowerChatIdentityDbContext>
    {
        protected override string ConnectionStringName { get; } = "IdentityDatabase";
        protected override string Project { get; } = "PowerChat.Services.Identity.API";

        protected override PowerChatIdentityDbContext CreateNewInstance(DbContextOptions<PowerChatIdentityDbContext> options)
        {
            return new PowerChatIdentityDbContext(options);
        }
    }
}