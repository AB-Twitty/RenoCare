using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;

namespace RenoCare.Persistence.Mapping
{
    /// <summary>
    /// Represents the session timetable entity configuration.
    /// </summary>
    public class SessionTimetableEntityConfiguration : IEntityTypeConfiguration<SessionTimetable>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<SessionTimetable> builder)
        {
            builder.ToTable("Sessions_Timetables");
            builder.HasKey(t => t.Id);

            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.Day).IsRequired().HasConversion<int>();
            builder.Property(x => x.Time).IsRequired().HasConversion<long>();
        }
    }
}
