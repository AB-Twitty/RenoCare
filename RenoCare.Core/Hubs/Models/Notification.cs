using System;

namespace RenoCare.Core.Hubs.Models
{
    public class Notification
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date => DateTime.Now;
        public string FormattedDate => Date.ToString("ddd MMM dd yyyy - hh:mm tt");
    }
}
