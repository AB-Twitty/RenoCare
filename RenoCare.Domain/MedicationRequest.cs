using RenoCare.Domain.Common;
using System;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents a dialysis session medication request entity.
    /// </summary>
    public class MedicationRequest : BaseEntity
    {
        /// <summary>
        /// Gets or sets the patient id
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Gets or sets the dilaysis unit id
        /// </summary>
        public int DialysisUnitId { get; set; }

        /// <summary>
        /// Gets or sets the appointment date for the dialysis session
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// Gets or sets the time of the appointment
        /// </summary>
        public string AppointmentHour { get; set; }

        /// <summary>
        /// Gets or sets extra information provided by the patient about his condition
        /// </summary>
        public string PatientProblem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the current status of the medication request
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the type of the medication request
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the report of treatment session
        /// </summary>
        public int? ReportId { get; set; }


        /// <summary>
        /// Gets or sets the navigation property for the patient of the medication request.
        /// </summary>
        public virtual Patient Patient { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the status of the medication request.
        /// </summary>
        public virtual MedicationRequestStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the type of the medication request.
        /// </summary>
        public virtual MedicationRequestType Type { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the report of the medication request.
        /// </summary>
        public virtual Report Report { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the requested dialysis unit.
        /// </summary>
        public virtual DialysisUnit DialysisUnit { get; set; }
    }
}
