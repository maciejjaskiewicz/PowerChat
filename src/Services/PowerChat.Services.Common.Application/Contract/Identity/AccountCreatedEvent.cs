using PowerChat.Services.Common.Application.Events;

namespace PowerChat.Services.Common.Application.Contract.Identity
{
    public class AccountCreatedEvent : IEvent
    {
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
    }
}