using RenoCare.Domain.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents an amenity entity.
    /// </summary>
    public class Amenity : BaseEntity
    {
        public Amenity() => DialysisUnits = new HashSet<DialysisUnit>();

        /// <summary>
        /// Gets or sets the amentity's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the amentity's icon
        /// </summary>
        public string Icon { get; set; }

        [JsonIgnore]
        public virtual ICollection<DialysisUnit> DialysisUnits { get; set; }
    }
}
