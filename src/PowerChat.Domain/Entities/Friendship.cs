using PowerChat.Domain.Common;

namespace PowerChat.Domain.Entities
{
    public class Friendship : AuditableEntity, IDeletableEntity
    {
        public User RequestedBy { get; set; }
        public long RequestedById { get; set; }
        public User RequestedTo { get; set; }
        public long RequestedToId { get; set; }
        public bool Approved { get; set; }
        public bool IsDeleted { get; set; }
    }
}
