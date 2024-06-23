using System;

namespace RenoCare.Core.Features.MedicationRequests.DTOs
{
    public class MedicationRequestListItemDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int PatientId { get; set; }
        public string DialysisUnitName { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int? ReportId { get; set; }
    }
}
