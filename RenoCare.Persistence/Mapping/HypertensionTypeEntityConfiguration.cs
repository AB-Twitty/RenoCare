using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;

namespace RenoCare.Persistence.Mapping
{
    /// <summary>
    /// Represents the hypertension type entity configuration.
    /// </summary>
    public class HypertensionTypeEntityConfiguration : IEntityTypeConfiguration<HypertensionType>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<HypertensionType> builder)
        {
            builder.ToTable("Hypertension_Types");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired();

            builder.HasData(
                new HypertensionType
                {
                    Id = 1,
                    Name = "Normal",
                    Description = "Systolic blood pressure less than 120 mm Hg and diastolic blood pressure less than 80 mm Hg."
                },
                new HypertensionType
                {
                    Id = 2,
                    Name = "Elevated",
                    Description = "Systolic blood pressure between 120-129 mm Hg and diastolic blood pressure less than 80 mm Hg."
                },
                new HypertensionType
                {
                    Id = 3,
                    Name = "Hypertension Stage 1",
                    Description = "Systolic blood pressure consistently ranging from 130-139 mm Hg or diastolic blood pressure consistently ranging from 80-89 mm Hg."
                },
                new HypertensionType
                {
                    Id = 4,
                    Name = "Hypertension Stage 2",
                    Description = "Systolic blood pressure of 140 mm Hg or higher or diastolic blood pressure of 90 mm Hg or higher."
                },
                new HypertensionType
                {
                    Id = 5,
                    Name = "Hypertensive Crisis:",
                    Description = " Blood pressure readings exceeding 180/120 mm Hg, requiring immediate medical attention."
                });
        }
    }
}
