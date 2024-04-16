namespace RenoCare.Core.Features.Patients.DTOs
{
    public class PatientListItemDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Diabetes { get; set; }
        public bool Hypertension { get; set; }
        public int ReportsCount { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}
