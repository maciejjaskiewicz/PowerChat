﻿using Microsoft.EntityFrameworkCore;

namespace PowerChat.Persistence
{
    public class PowerChatDbContextFactory : DesignTimeDbContextFactoryBase<PowerChatDbContext>
    {
        protected override string ConnectionStringName { get; } = "PowerChatDatabase";
        protected override string Project { get; } = "PowerChat.API";

        protected override PowerChatDbContext CreateNewInstance(DbContextOptions<PowerChatDbContext> options)
        {
            return new PowerChatDbContext(options);
        }
    }
}
