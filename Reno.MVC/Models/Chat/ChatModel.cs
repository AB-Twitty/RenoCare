using Reno.MVC.Services.Base;
using System.Collections.Generic;

namespace Reno.MVC.Models.Chat
{
    public class ChatModel
    {
        public IList<ContactDto> Contacts { get; set; }

        public ContactDto ActiveContact { get; set; }

        public IList<ChatMessage> Messages { get; set; }
    }
}
