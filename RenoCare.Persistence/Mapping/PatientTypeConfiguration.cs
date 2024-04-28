using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;

namespace RenoCare.Persistence.Mapping
{
    /// <summary>
    /// Represents the patient entity configuration.
    /// </summary>
    public class PatientTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId).IsRequired();
            builder.HasOne(p => p.User).WithOne().HasForeignKey<Patient>(p => p.UserId);

            builder.Property(p => p.KidneyFailureCause).IsRequired();

            builder.Property(p => p.DiabetesTypeId).IsRequired();
            builder.HasOne(p => p.DiabetesType).WithOne().HasForeignKey<Patient>(p => p.DiabetesTypeId);

            builder.Property(p => p.HypertensionTypeId).IsRequired();
            builder.HasOne(p => p.HypertensionType).WithOne().HasForeignKey<Patient>(p => p.HypertensionTypeId);

            builder.Property(p => p.SmokingStatusId);
            builder.HasOne(p => p.SmokingStatus).WithOne().HasForeignKey<Patient>(p => p.SmokingStatusId);


            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.HasData(
                new Patient
                {
                    Id = 1,
                    UserId = "a6d6f491-1957-4e70-98c7-997eb0d3255f",
                    KidneyFailureCause = "Hypertension",
                    DiabetesTypeId = 1,
                    HypertensionTypeId = 1
                });
        }
    }
}
