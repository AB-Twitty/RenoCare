using System;

namespace RenoCare.Core.Features.DialysisUnits.Dtos
{
    public class SessionTimeDto
    {
        public DayOfWeek Day { get; set; }
        public DateTime Time { get; set; }
    }
}
