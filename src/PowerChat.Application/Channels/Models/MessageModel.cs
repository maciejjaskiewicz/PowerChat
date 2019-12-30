using System;

namespace PowerChat.Application.Channels.Models
{
    public class MessageModel
    {
        public long Id { get; set; }
        public long AuthorId { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime? Seen { get; set; }
        public bool Own { get; set; }
    }
}
