using RenoCare.Domain.Common;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents a medication request type.
    /// </summary>
    public class MedicationRequestType : BaseEntity
    {
        /// <summary>
        /// Gets or sets the medication request type name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a short description of the medication request type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indecating whether an entity is active or not
        /// </summary>
        public bool IsActive { get; set; }
    }
}
