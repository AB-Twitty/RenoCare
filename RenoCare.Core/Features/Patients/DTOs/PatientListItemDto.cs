using System;
using System.Collections.Generic;

namespace RenoCare.Core.Features.Patients.DTOs
{
    public class PatientListItemDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string Diabetes { get; set; }
        public string Hypertension { get; set; }
        public int ReportsSameUnit { get; set; }
        public int ReportsOverral { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Age { get; set; }
        public string Smoking { get; set; }
        public string Viruses { get; set; }
        public IDictionary<string, int> MedReqCnts { get; set; }
    }
}
