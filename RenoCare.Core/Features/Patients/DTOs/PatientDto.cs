using System;

namespace RenoCare.Core.Features.Patients.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string Diabetes { get; set; }
        public string Hypertension { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Smoking { get; set; }
    }
}
