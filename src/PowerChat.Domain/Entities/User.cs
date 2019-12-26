using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using PowerChat.Domain.Common;
using PowerChat.Domain.Enums;
using PowerChat.Domain.ValueObjects;

namespace PowerChat.Domain.Entities
{
    public class User : IdentityUser<long>, IAuditableEntity, IDeletableEntity
    {
        public PersonName Name { get; set; }
        public Gender? Gender { get; set; }
        public string About { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ICollection<Friendship> SentFriendshipRequests { get; set; }
        public ICollection<Friendship> ReceivedFriendshipsRequests { get; set; }
    }
}
