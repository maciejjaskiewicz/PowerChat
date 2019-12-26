using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Common.Interfaces;
using PowerChat.Domain.Common;
using PowerChat.Domain.Entities;
using PowerChat.Persistence.Configurations.Extensions;

namespace PowerChat.Persistence
{
    public class PowerChatDbContext : IdentityDbContext<User, IdentityRole<long>, long>, IPowerChatDbContext
    {
        public DbSet<Channel> Channels { get; set;  }
        public DbSet<Friendship> Friendships { get; set; }

        private readonly IDateTime _dateTime;

        public PowerChatDbContext(DbContextOptions options) 
            : base(options)
        { }

        public PowerChatDbContext(DbContextOptions options, IDateTime dateTime) 
            : base(options)
        {
            _dateTime = dateTime;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = _dateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(PowerChatDbContext).Assembly);
            builder.ApplyIdentityEntitiesConfiguration();
        }
    }
}
