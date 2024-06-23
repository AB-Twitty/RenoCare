using RenoCare.Core.Features.Patients.DTOs;
using System;

namespace RenoCare.Core.Features.Reports.Dtos
{
    public class ReportDto
    {
        public int Id { get; set; }
        public PatientDto Patient { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime SessionDate { get; set; }

        #region Session Details

        public string DialysisUnitName { get; set; }
        public string Nephrologist { get; set; }
        public double DialysisDuration { get; set; }
        public int DialysisFrequency { get; set; }
        public string VascularAccessType { get; set; }
        public string DialyzerType { get; set; }

        #endregion

        #region Vital Signs
        public double PreWeight { get; set; }
        public double PostWeight { get; set; }
        public string PreSystolicBloodPressure { get; set; }
        public string DuringSystolicBloodPressure { get; set; }
        public string PostSystolicBloodPressure { get; set; }
        public string PreDiastolicBloodPressure { get; set; }
        public string DuringDiastolicBloodPressure { get; set; }
        public string PostDiastolicBloodPressure { get; set; }
        public double DryWeight { get; set; }
        public int HeartRate { get; set; }

        #endregion

        #region Dialysis Treatment Outcomes

        public double PreUrea { get; set; }
        public double PostUrea { get; set; }
        public double UreaReductionRatio { get; set; }
        public double TotalFluidRemoval { get; set; }
        public double FluidRemovalRate { get; set; }
        public double UrineOutput { get; set; }
        public double Kt_V { get; set; }

        #endregion

        #region Para-CLinical Examinations

        public double Creatinine { get; set; }
        public double Potassium { get; set; }
        public double Hemoglobin { get; set; }
        public double Hematocrit { get; set; }
        public double Albumin { get; set; }

        #endregion

    }
}
