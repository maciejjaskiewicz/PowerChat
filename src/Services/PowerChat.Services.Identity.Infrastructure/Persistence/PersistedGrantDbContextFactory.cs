using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Common.Infrastructure.Persistence;

namespace PowerChat.Services.Identity.Infrastructure.Persistence
{
    public class PersistedGrantDbContextFactory : DesignTimeDbContextFactoryBase<PersistedGrantDbContext>
    {
        protected override string ConnectionStringName { get; } = "IdentityDatabase";
        protected override string Project { get; } = "PowerChat.Services.Identity.API";

        protected override PersistedGrantDbContext CreateNewInstance(DbContextOptions<PersistedGrantDbContext> options)
        {
            return new PersistedGrantDbContext(options, new OperationalStoreOptions());
        }
    }
}
