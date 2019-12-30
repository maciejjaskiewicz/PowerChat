using System.Collections.Generic;
using PowerChat.Domain.Common;

namespace PowerChat.Domain.Entities
{
    public class Channel : AuditableEntity, IDeletableEntity
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
        public ICollection<Interlocutor> Interlocutors { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
