using RenoCare.Domain.Common;
using System;
using System.Text.Json.Serialization;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents a session time and date availability entity.
    /// </summary>
    public class SessionTimetable : BaseEntity, ISoftDeletedEntity
    {
        /// <summary>
        /// Gets or sets the dialysis unit id.
        /// </summary>
        public int DialysisUnitId { get; set; }

        /// <summary>
        /// Gets or sets the day of the session
        /// </summary>
        public DayOfWeek Day { get; set; }

        /// <summary>
        /// Gets or sets the time of the session
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Gets or sets a value indecating whether an entity is deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }


        /// <summary>
        /// Gets or sets the navigation property for the dialysis unit.
        /// </summary>
        [JsonIgnore]
        public virtual DialysisUnit DialysisUnit { get; set; }

    }
}
