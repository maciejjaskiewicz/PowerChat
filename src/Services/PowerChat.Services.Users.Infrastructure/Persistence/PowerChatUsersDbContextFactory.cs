using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Common.Infrastructure.Persistence;

namespace PowerChat.Services.Users.Infrastructure.Persistence
{
    public class PowerChatUsersDbContextFactory : DesignTimeDbContextFactoryBase<PowerChatUsersDbContext>
    {
        protected override string ConnectionStringName { get; } = "UsersDatabase";
        protected override string Project { get; } = "PowerChat.Services.Users.API";

        protected override PowerChatUsersDbContext CreateNewInstance(DbContextOptions<PowerChatUsersDbContext> options)
        {
            return new PowerChatUsersDbContext(options);
        }
    }
}
