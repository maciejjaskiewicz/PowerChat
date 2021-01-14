using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerChat.Services.Chat.Core.Entities;
using PowerChat.Services.Chat.Core.Enums;

namespace PowerChat.Services.Chat.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : EntityConfigurationBase<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.IdentityId)
                .IsRequired()
                .HasMaxLength(36);

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
        }
    }
}
