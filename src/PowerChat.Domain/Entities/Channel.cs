using PowerChat.Domain.Common;

namespace PowerChat.Domain.Entities
{
    public class Channel : AuditableEntity, IDeletableEntity
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
