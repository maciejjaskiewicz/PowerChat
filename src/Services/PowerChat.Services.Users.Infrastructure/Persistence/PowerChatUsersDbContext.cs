using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PowerChat.Common.Domain;
using PowerChat.Common.Interfaces;
using PowerChat.Services.Users.Application.Services;
using PowerChat.Services.Users.Core.Entities;

namespace PowerChat.Services.Users.Infrastructure.Persistence
{
    public class PowerChatUsersDbContext : DbContext, IPowerChatServiceDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Friendship> Friendships { get; set; }

        private readonly IDateTime _dateTime;

        public PowerChatUsersDbContext(DbContextOptions options)
            : base(options)
        { }

        public PowerChatUsersDbContext(DbContextOptions options, IDateTime dateTime)
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

            builder.ApplyConfigurationsFromAssembly(typeof(PowerChatUsersDbContext).Assembly);
        }
    }
}