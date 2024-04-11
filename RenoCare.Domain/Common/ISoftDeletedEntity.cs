namespace RenoCare.Domain.Common
{
    /// <summary>
	/// Represents a soft deleted entity (without actually deleting it from storage) 
	/// </summary>
	public interface ISoftDeletedEntity
    {
        /// <summary>
        /// Gets or sets a value indecating whether an entity is deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
