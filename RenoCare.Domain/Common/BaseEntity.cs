namespace RenoCare.Domain.Common
{
    /// <summary>
	/// Represents the base class for entities
	/// </summary>
	public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier (primary key)
        /// </summary>
        public int Id { get; set; }
    }
}
