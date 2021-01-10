using PowerChat.Common.Domain;

namespace PowerChat.Services.Chat.Core.Entities
{
    public class Interlocutor : AuditableEntity, IDeletableEntity
    {
        public Channel Channel { get; set; }
        public long ChannelId { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
