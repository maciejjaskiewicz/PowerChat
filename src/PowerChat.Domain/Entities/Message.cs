using System;
using PowerChat.Domain.Common;

namespace PowerChat.Domain.Entities
{
    public class Message : AuditableEntity, IDeletableEntity
    {
        public Channel Channel { get; set; }
        public long ChannelId { get; set; }
        public User Sender { get; set; }
        public long SenderId { get; set; }
        public string Content { get; set; }
        public DateTime? Seen { get; set; }
        public bool IsDeleted { get; set; }
    }
}
