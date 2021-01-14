using System;
using System.Collections.Generic;
using PowerChat.Common.Domain;
using PowerChat.Services.Users.Core.Enums;
using PowerChat.Services.Users.Core.ValueObjects;

namespace PowerChat.Services.Users.Core.Entities
{
    public class User : AuditableEntity, IDeletableEntity
    {
        public string IdentityId { get; set; }
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
