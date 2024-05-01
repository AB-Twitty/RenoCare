using RenoCare.Domain.Common;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents a dialysis session medication request satus entity.
    /// </summary>
    public class MedicationRequestStatus : BaseEntity
    {
        /// <summary>
        /// Gets or sets the appointment status name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a short description of the appointment status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a the bootstrap label class color for the status.
        /// </summary>
        public string LabelClass { get; set; }
    }
}
