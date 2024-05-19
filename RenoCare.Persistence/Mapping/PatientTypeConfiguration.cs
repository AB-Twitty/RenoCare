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
            builder.ToTable(nameof(Patient));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId).IsRequired();
            builder.HasOne(p => p.User).WithOne().HasForeignKey<Patient>(p => p.UserId);

            builder.Property(p => p.KidneyFailureCause).IsRequired();

            builder.Property(p => p.DiabetesType).IsRequired();

            builder.Property(p => p.HypertensionType).IsRequired();

            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);

           
        }
    }
}
