using RenoCare.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

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
        [Range(1.0, 8.0)]
        public double DialysisDuration { get; set; }

        /// <summary>
        /// Gets or sets the frequency per week for the dialysis treatment session.
        /// </summary>
        [Range(1, 7)]
        public int DialysisFrequency { get; set; }

        /// <summary>
        /// Gets or sets the vascular access type.
        /// </summary>
        public VascularType VascularAccessType { get; set; }

        /// <summary>
        /// Gets or sets the dialyzer type.
        /// </summary>
        public DialyzerType DialyzerType { get; set; }

        #endregion

        #region Vital Signs

        /// <summary>
        /// Gets or sets the pre dialysis session weight of patient.
        /// </summary>
        [Range(30.0, 200.0)]
        public double PreWeight { get; set; }

        /// <summary>
        /// Gets or sets the post dialysis session weight of patient.
        /// </summary>
        [Range(30.0, 200.0)]
        public double PostWeight { get; set; }

        /// <summary>
        /// Gets or sets the systolic blood pressure of patient pre dialysis session .
        /// </summary>
        public string PreSystolicBloodPressure { get; set; }

        /// <summary>
        /// Gets or sets the systolic blood pressure of patient during dialysis session .
        /// </summary>
        public string DuringSystolicBloodPressure { get; set; }

        /// <summary>
        /// Gets or sets the systolic blood pressure of patient post dialysis session .
        /// </summary>
        public string PostSystolicBloodPressure { get; set; }

        /// <summary>
        /// Gets or sets the diastolic blood pressure of patient pre dialysis session .
        /// </summary>
        public string PreDiastolicBloodPressure { get; set; }

        /// <summary>
        /// Gets or sets the systolic blood pressure of patient during dialysis session .
        /// </summary>
        public string DuringDiastolicBloodPressure { get; set; }

        /// <summary>
        /// Gets or sets the systolic blood pressure of patient post dialysis session .
        /// </summary>
        public string PostDiastolicBloodPressure { get; set; }

        /// <summary>
        /// Gets or sets the dry weight.
        /// </summary>
        [Range(30.0, 200.0)]
        public double DryWeight { get; set; }

        /// <summary>
        /// Gets or sets the heart rate of patient.
        /// </summary>
        [Range(40, 120)]
        public int HeartRate { get; set; }

        #endregion

        #region Dialysis Treatment Outcomes

        /// <summary>
        /// Gets or sets the urea pre dialysis session.
        /// </summary>
        [Range(5.0, 200.0)]
        public double PreUrea { get; set; }

        /// <summary>
        /// Gets or sets the urea post dialysis session.
        /// </summary>
        [Range(5.0, 200.0)]
        public double PostUrea { get; set; }

        /// <summary>
        /// Gets or sets the urea reduction ratio (URR).
        /// </summary>
        [Range(0.0, 100.0)]
        public double UreaReductionRatio { get; set; }

        /// <summary>
        /// Gets or sets the total fluid removal.
        /// </summary>
        [Range(0.0, 5000.0)]
        public double TotalFluidRemoval { get; set; }

        /// <summary>
        /// Gets or sets the fluid removal rate.
        /// </summary>
        [Range(0.0, 5000.0)]
        public double FluidRemovalRate { get; set; }

        /// <summary>
        /// Gets or sets the urine output per day.
        /// </summary>
        [Range(0.0, 5000.0)]
        public double UrineOutput { get; set; }

        /// <summary>
        /// Gets or sets the Kt/V value.
        /// </summary>
        [Range(0.5, 2.5)]
        public double Kt_V { get; set; }

        #endregion

        #region Para-Clinical Examinations

        /// <summary>
        /// Gets or sets the creatinine.
        /// </summary>
        [Range(0.5, 15.0)]
        public double Creatinine { get; set; }

        /// <summary>
        /// Gets or sets the Potassium.
        /// </summary>
        [Range(2.5, 6.5)]
        public double Potassium { get; set; }

        /// <summary>
        /// Gets or sets the hemoglobin.
        /// </summary>
        [Range(5.0, 20.0)]
        public double Hemoglobin { get; set; }

        /// <summary>
        /// Gets or sets the Hematocrit.
        /// </summary>
        [Range(15.0, 60.0)]
        public double Hematocrit { get; set; }

        /// <summary>
        /// Gets or sets the Albumin.
        /// </summary>
        [Range(2.0, 6.0)]
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
