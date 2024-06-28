using RenoCare.Domain.Common;
using System;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents a dialysis treatment session report entity.
    /// </summary>
    public class Report : BaseEntity
    {
        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Gets or sets the dialysis unit id.
        /// </summary>
        public int DialysisUnitId { get; set; }

        /// <summary>
        /// Gets or sets the medication request id for the treatment session.
        /// </summary>
        public int MedicationRequestId { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the report.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last modification date of the report.
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        #region Session Details

        /// <summary>
        /// Gets or sets the name of the nephrologist.
        /// </summary>
        public string Nephrologist { get; set; }

        /// <summary>
        /// Gets or sets the duration in hours for the dialysis treatment session.
        /// </summary>
        public double DialysisDuration { get; set; }

        /// <summary>
        /// Gets or sets the frequency per week for the dialysis treatment session.
        /// </summary>
        public int DialysisFrequency { get; set; }

        /// <summary>
        /// Gets or sets the vascular access type.
        /// </summary>
        public VascularType VascularAccessType { get; set; }

        /// <summary>
        /// Gets or sets the dialyzer type.
        /// </summary>
        public DialyzerType DialyzerType { get; set; }

        public string GeneralRemarks { get; set; }

        #endregion

        #region Vital Signs

        /// <summary>
        /// Gets or sets the pre dialysis session weight of patient.
        /// </summary>
        public double PreWeight { get; set; }

        /// <summary>
        /// Gets or sets the post dialysis session weight of patient.
        /// </summary>
        public double PostWeight { get; set; }

        /// <summary>
        /// Gets or sets the blood pressure of patient pre dialysis session .
        /// </summary>
        public string PreBloodPressure { get; set; }

        /// <summary>
        /// Gets or sets the blood pressure of patient during dialysis session .
        /// </summary>
        public string DuringBloodPressure { get; set; }

        /// <summary>
        /// Gets or sets the blood pressure of patient post dialysis session .
        /// </summary>
        public string PostBloodPressure { get; set; }

        /// <summary>
        /// Gets or sets the dry weight.
        /// </summary>
        public double DryWeight { get; set; }

        /// <summary>
        /// Gets or sets the heart rate of patient.
        /// </summary>
        public int HeartRate { get; set; }

        #endregion

        #region Dialysis Treatment Outcomes

        /// <summary>
        /// Gets or sets the urea pre dialysis session.
        /// </summary>
        public double PreUrea { get; set; }

        /// <summary>
        /// Gets or sets the urea post dialysis session.
        /// </summary>
        public double PostUrea { get; set; }

        /// <summary>
        /// Gets or sets the urea reduction ratio (URR).
        /// </summary>
        public double UreaReductionRatio { get; set; }

        /// <summary>
        /// Gets or sets the total fluid removal.
        /// </summary>
        public double TotalFluidRemoval { get; set; }

        /// <summary>
        /// Gets or sets the fluid removal rate.
        /// </summary>
        public double FluidRemovalRate { get; set; }

        /// <summary>
        /// Gets or sets the urine output per day.
        /// </summary>
        public double UrineOutput { get; set; }

        /// <summary>
        /// Gets or sets the Kt/V value.
        /// </summary>
        public double Kt_V { get; set; }

        #endregion

        #region Para-Clinical Examinations

        /// <summary>
        /// Gets or sets the creatinine.
        /// </summary>
        public double Creatinine { get; set; }

        /// <summary>
        /// Gets or sets the Potassium.
        /// </summary>
        public double Potassium { get; set; }

        /// <summary>
        /// Gets or sets the hemoglobin.
        /// </summary>
        public double Hemoglobin { get; set; }

        /// <summary>
        /// Gets or sets the Hematocrit.
        /// </summary>
        public double Hematocrit { get; set; }

        /// <summary>
        /// Gets or sets the Albumin.
        /// </summary>
        public double Albumin { get; set; }

        #endregion


        /// <summary>
        /// Gets or sets the navigation property for the patient.
        /// </summary>
        public virtual Patient Patient { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the medication request.
        /// </summary>
        public virtual MedicationRequest MedicationRequest { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the dialysis unit.
        /// </summary>
        public virtual DialysisUnit DialysisUnit { get; set; }
    }

    public enum VascularType
    {
        Fistula,
        Graft,
        Catheter
    }

    public enum DialyzerType
    {
        HighFlux,
        LowFlux
    }
}
