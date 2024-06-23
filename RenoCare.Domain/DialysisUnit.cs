using RenoCare.Domain.Common;
using RenoCare.Domain.Identity;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents a dialysis unit information entity.
    /// </summary>
    public class DialysisUnit : BaseEntity, ISoftDeletedEntity
    {
        /// <summary>
        /// Gets or sets the user account id for the manager of the dialysis unit
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the dialysis unit name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a description for the dialysis unit name
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the dialysis unit
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the address of the dialysis unit
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the country of the dialysis unit
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the city of the dialysis unit
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets a valie indicating whether HD treatnment is supported
        /// </summary>
        public bool IsHDSupported { get; set; }

        /// <summary>
        /// Gets or sets a valie indicating whether HDF treatnment is supported
        /// </summary>
        public bool IsHDFSupported { get; set; }

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
