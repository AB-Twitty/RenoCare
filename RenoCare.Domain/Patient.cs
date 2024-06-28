using RenoCare.Domain.Common;
using RenoCare.Domain.Identity;
using System;
using System.Collections.Generic;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents a patient entity.
    /// </summary>
    public class Patient : BaseEntity, ISoftDeletedEntity
    {
        /// <summary>
        /// Gets or sets the user account id for the patient
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the cause of kidney failure for the patient
        /// </summary>
        public string KidneyFailureCause { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the diabetes type of the patient
        /// </summary>
        public int DiabetesTypeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the hypertension type of the patient
        /// </summary>
        public int HypertensionTypeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating The smoking status of the patient
        /// </summary>
        public int? SmokingStatusId { get; set; }

        /// <summary>
        /// Gets or sets the reason of deletion
        /// </summary>
        public string DeletionReason { get; set; }

        /// <summary>
        /// Gets or sets a value indecating whether an entity is deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }


        /// <summary>
        /// Gets or sets the navigation property for hypertension type.
        /// </summary>
        public virtual HypertensionType HypertensionType { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for diabetes type.
        /// </summary>
        public virtual DiabetesType DiabetesType { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for smoking status.
        /// </summary>
        public virtual SmokingStatus SmokingStatus { get; set; }

        /// <summary>
        /// Gets or sets the navigation property user account
        /// </summary>
        public virtual AppUser User { get; set; }

        public virtual ICollection<Virus> Viruses { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}
