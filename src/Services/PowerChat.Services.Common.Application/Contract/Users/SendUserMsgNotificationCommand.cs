using System;
using PowerChat.Services.Common.Application.Commands;

namespace PowerChat.Services.Common.Application.Contract.Users
{
    public class SendUserMsgNotificationCommand : ICommand
    {
        public string UserIdentityId { get; set; }
        public long ChannelId { get; set; }
        public MsgNotificationModel MsgNotification { get; set; }
    }

    public class MsgNotificationModel
    {
        public long Id { get; set; }
        public long AuthorId { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime? Seen { get; set; }
        public bool Own { get; set; }
    }
}
