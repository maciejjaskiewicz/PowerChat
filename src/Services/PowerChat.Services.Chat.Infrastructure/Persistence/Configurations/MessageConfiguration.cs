using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerChat.Services.Chat.Core.Entities;

namespace PowerChat.Services.Chat.Infrastructure.Persistence.Configurations
{
    public class MessageConfiguration : EntityConfigurationBase<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            base.Configure(builder);

            builder.HasOne(m => m.Channel)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChannelId)
                .IsRequired();

            builder.HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .IsRequired();
        }
    }
}
