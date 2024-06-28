using RenoCare.Domain;
using System;

namespace RenoCare.Core.Features.Patients.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string Diabetes { get; set; }
        public string Hypertension { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Age { get; set; }
        public string Smoking { get; set; }
        public string Viruses { get; set; }
    }
}
