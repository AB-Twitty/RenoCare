using System;

namespace RenoCare.Core.Features.DialysisUnits.Dtos
{
    public class SessionTimeDto
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public DateTime Time { get; set; }
        public string FormattedTime => Time.ToString("hh:mm tt");
    }
}
