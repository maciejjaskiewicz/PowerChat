using System;

namespace PowerChat.Application.Channels.Queries.GetChannelsList.Models
{
    public class ChannelPreviewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string LastMessage { get; set; }
        public DateTime? LastMessageDate { get; set; }
        public bool Seen { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
