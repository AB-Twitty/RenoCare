﻿using RenoCare.Domain.Common;
using RenoCare.Domain.Identity;
using System;

namespace RenoCare.Domain.Chat
{
    public class ChatMessage : BaseEntity, ISoftDeletedEntity
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }
        public DateTime SendingTime { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFile { get; set; }
        public string FileLink { get; set; }


        public virtual AppUser Sender { get; set; }
        public virtual AppUser Receiver { get; set; }
    }
}
