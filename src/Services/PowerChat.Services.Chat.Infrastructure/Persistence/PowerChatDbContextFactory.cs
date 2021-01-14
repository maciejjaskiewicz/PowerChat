using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Common.Infrastructure.Persistence;

namespace PowerChat.Services.Chat.Infrastructure.Persistence
{
    public class PowerChatDbContextFactory : DesignTimeDbContextFactoryBase<PowerChatDbContext>
    {
        protected override string ConnectionStringName { get; } = "ChatDatabase";
        protected override string Project { get; } = "PowerChat.Services.Chat.API";

        protected override PowerChatDbContext CreateNewInstance(DbContextOptions<PowerChatDbContext> options)
        {
            return new PowerChatDbContext(options);
        }
    }
}
