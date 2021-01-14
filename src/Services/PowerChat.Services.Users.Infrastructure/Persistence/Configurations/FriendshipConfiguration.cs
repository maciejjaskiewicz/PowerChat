using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerChat.Services.Users.Core.Entities;

namespace PowerChat.Services.Users.Infrastructure.Persistence.Configurations
{
    public class FriendshipConfiguration : EntityConfigurationBase<Friendship>
    {
        public override void Configure(EntityTypeBuilder<Friendship> builder)
        {
            base.Configure(builder);

            builder.HasOne(f => f.RequestedBy)
                .WithMany(u => u.SentFriendshipRequests)
                .HasForeignKey(f => f.RequestedById)
                .IsRequired();

            builder.HasOne(f => f.RequestedTo)
                .WithMany(u => u.ReceivedFriendshipsRequests)
                .HasForeignKey(f => f.RequestedToId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
