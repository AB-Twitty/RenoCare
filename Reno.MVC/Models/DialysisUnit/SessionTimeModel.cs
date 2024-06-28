using System;

namespace Reno.MVC.Models.DialysisUnit
{
    public class SessionTimeModel
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan Time { get; set; }
        public bool Deleted { get; set; }
    }
}
