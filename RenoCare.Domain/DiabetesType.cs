using RenoCare.Domain.Common;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents a diabetes categories.
    /// </summary>
    public class DiabetesType : BaseEntity
    {
        /// <summary>
        /// Gets or sets the diabetes type name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a short description of the diabetes type.
        /// </summary>
        public string Description { get; set; }
    }
}
