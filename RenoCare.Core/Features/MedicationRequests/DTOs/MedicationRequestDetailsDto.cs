namespace RenoCare.Core.Features.MedicationRequests.DTOs
{
    public class MedicationRequestDetailsDto
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public string DialysisUnitName { get; set; }
        public string Status { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string FormattedAdress { get; set; }
        public int? ReportId { get; set; }
    }
}
