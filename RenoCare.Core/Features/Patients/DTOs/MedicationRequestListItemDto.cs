using System;

namespace RenoCare.Core.Features.Patients.DTOs
{
    public class MedicationRequestListItemDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string DialysisUnitName { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
