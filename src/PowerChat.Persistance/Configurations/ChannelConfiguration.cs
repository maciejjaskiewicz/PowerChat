using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerChat.Domain.Entities;

namespace PowerChat.Persistence.Configurations
{
    public class ChannelConfiguration : EntityConfigurationBase<Channel>
    {
        public override void Configure(EntityTypeBuilder<Channel> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .HasMaxLength(256);
        }
    }
}
