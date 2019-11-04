using Microsoft.EntityFrameworkCore;

namespace PowerChat.Persistence
{
    public class PowerChatDbContextFactory : DesignTimeDbContextFactoryBase<PowerChatDbContext>
    {
        protected override PowerChatDbContext CreateNewInstance(DbContextOptions<PowerChatDbContext> options)
        {
            return new PowerChatDbContext(options);
        }
    }
}
