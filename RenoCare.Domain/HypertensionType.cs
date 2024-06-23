using RenoCare.Domain.Common;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents a hypertension (blood pressure) categories.
    /// </summary>
    public class HypertensionType : BaseEntity
    {
        /// <summary>
        /// Gets or sets the hypertension type name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a short description of the hypertension type.
        /// </summary>
        public string Description { get; set; }
    }
}
