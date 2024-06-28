using RenoCare.Domain.Common;
using RenoCare.Domain.Identity;
using System;
using System.Collections.Generic;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents a dialysis unit information entity.
    /// </summary>
    public class DialysisUnit : BaseEntity, ISoftDeletedEntity
    {
        public DialysisUnit()
        {
            Amenities = new HashSet<Amenity>();
            Sessions = new HashSet<SessionTimetable>();
            Images = new HashSet<Image>();
            AcceptingViruses = new HashSet<Virus>();
        }

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
        /// Gets or sets a value indicating whether HD treatment is supported
        /// </summary>
        public bool IsHdSupported { get; set; }

        /// <summary>
        /// Gets or sets the price of HD treatment session
        /// </summary>
        public double? HdPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether HDF treatment is supported
        /// </summary>
        public bool IsHdfSupported { get; set; }

        /// <summary>
        /// Gets or sets the price of HDF treatment session
        /// </summary>
        public double? HdfPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indecating whether an entity is deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }


        /// <summary>
        /// Gets or sets the navigation property user account
        /// </summary>
        public virtual AppUser User { get; set; }

        /// <summary>
        /// Gets or sets the navigation property amenities
        /// </summary>
        public virtual ICollection<Amenity> Amenities { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for sessions
        /// </summary>
        public virtual ICollection<SessionTimetable> Sessions { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for dialysis unit images
        /// </summary>
        public virtual ICollection<Image> Images { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for medication requests
        /// </summary>
        public virtual ICollection<MedicationRequest> MedRequests { get; set; }


        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Virus> AcceptingViruses { get; set; }
    }
}
