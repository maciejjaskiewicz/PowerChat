﻿namespace PowerChat.Application.Friends.Queries.GetFriends.Models
{
    public class FriendModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public bool Approved { get; set; }
    }
}
