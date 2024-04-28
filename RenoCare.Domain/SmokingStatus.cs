using RenoCare.Domain.Common;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents smoking status entity.
    /// </summary>
    public class SmokingStatus : BaseEntity
    {
        /// <summary>
        /// Gets or sets the smoking status name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a short description of the smoking status.
        /// </summary>
        public string Description { get; set; }
    }
}
