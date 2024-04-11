using RenoCare.Domain.Common;
using RenoCare.Domain.Identity;

namespace RenoCare.Domain
{
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
        /// Gets or sets a vlaue indicating whether the patient has diabetes or not
        /// </summary>
        public bool Diabetes { get; set; }

        /// <summary>
        /// Gets or sets a vlaue indicating whether the patient has hypertension (high blood pressure) or not
        /// </summary>
        public bool Hypertension { get; set; }

        /// <summary>
        /// Gets or sets a vlaue indicating whether the patient smokes or not
        /// </summary>
        public bool Smoking { get; set; }

        /// <summary>
        /// Gets or sets the reason of deletion
        /// </summary>
        public string DeletionReason { get; set; }

        /// <summary>
        /// Gets or sets a value indecating whether an entity is deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the navigation property user account
        /// </summary>
        public virtual AppUser User { get; set; }
    }
}
