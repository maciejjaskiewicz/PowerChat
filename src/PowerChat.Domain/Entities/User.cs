using System;
using System.Collections.Generic;
using PowerChat.Domain.Common;
using PowerChat.Domain.Enums;
using PowerChat.Domain.ValueObjects;

namespace PowerChat.Domain.Entities
{
    public class User : AuditableEntity, IDeletableEntity
    {
        public Guid IdentityId { get; set; }
        public string Email { get; set; }
        public PersonName Name { get; set; }
        public Gender? Gender { get; set; }
        public string About { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastActive { get; set; }
        public ICollection<Friendship> SentFriendshipRequests { get; set; }
        public ICollection<Friendship> ReceivedFriendshipsRequests { get; set; }
    }
}
