using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerChat.Services.Chat.Core.Entities;

namespace PowerChat.Services.Chat.Infrastructure.Persistence.Configurations
{
    public  class InterlocutorConfiguration : EntityConfigurationBase<Interlocutor>
    {
        public override void Configure(EntityTypeBuilder<Interlocutor> builder)
        {
            base.Configure(builder);

            builder.HasOne(i => i.Channel)
                .WithMany(c => c.Interlocutors)
                .HasForeignKey(i => i.ChannelId)
                .IsRequired();

            builder.HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .IsRequired();
        }
    }
}
