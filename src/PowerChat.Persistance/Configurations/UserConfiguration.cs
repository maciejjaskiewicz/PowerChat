using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerChat.Domain.Entities;
using PowerChat.Domain.Enums;

namespace PowerChat.Persistence.Configurations
{
    public class UserConfiguration : EntityConfigurationBase<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(m => m.Name, a =>
            {
                a.Property(x => x.FirstName)
                    .HasMaxLength(256);
                a.Property(x => x.LastName)
                    .HasMaxLength(256);
            });

            builder.Property(x => x.Gender)
                .HasMaxLength(256)
                .HasConversion(EnumConverter<Gender>());

            builder.HasMany(u => u.SentFriendshipRequests)
                .WithOne(f => f.RequestedBy)
                .HasForeignKey(f => f.RequestedById);

            builder.HasMany(u => u.ReceivedFriendshipsRequests)
                .WithOne(f => f.RequestedTo)
                .HasForeignKey(f => f.RequestedToId);
        }
    }
}
