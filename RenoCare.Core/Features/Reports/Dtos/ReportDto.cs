using RenoCare.Core.Features.Patients.DTOs;
using RenoCare.Domain;
using System;

namespace RenoCare.Core.Features.Reports.Dtos
{
    public class ReportDto
    {
        public int Id { get; set; }
        public int MedReqId { get; set; }
        public int DialysisUnitId { get; set; }
        public PatientDto Patient { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime SessionDate { get; set; }

        #region Session Details

        public string DialysisUnitName { get; set; }
        public string Nephrologist { get; set; }
        public double DialysisDuration { get; set; }
        public int DialysisFrequency { get; set; }
        public VascularType VascularAccessType { get; set; }
        public DialyzerType DialyzerType { get; set; }
        public string GeneralRemarks { get; set; }

        #endregion

        #region Vital Signs
        public double PreWeight { get; set; }
        public double PostWeight { get; set; }
        public string PreBloodPressure { get; set; }
        public string DuringBloodPressure { get; set; }
        public string PostBloodPressure { get; set; }
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
