using System;
using PowerChat.Common.Domain;
using PowerChat.Services.Chat.Core.Enums;
using PowerChat.Services.Chat.Core.ValueObjects;

namespace PowerChat.Services.Chat.Core.Entities
{
    public class User : AuditableEntity, IDeletableEntity
    {
        public string IdentityId { get; set; }
        public string Email { get; set; }
        public PersonName Name { get; set; }
        public Gender? Gender { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastActive { get; set; }
    }
}
