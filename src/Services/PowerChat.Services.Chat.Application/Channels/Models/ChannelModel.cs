using System;
using System.Collections.Generic;

namespace PowerChat.Services.Chat.Application.Channels.Models
{
    public class ChannelModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public InterlocutorModel Interlocutor { get; set; }
        public IEnumerable<MessageModel> Messages { get; set; }
        public DateTime? LastActive { get; set; }
        public bool IsOnline { get; set; }
    }
}
