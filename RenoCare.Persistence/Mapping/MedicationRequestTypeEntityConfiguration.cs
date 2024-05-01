using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;

namespace RenoCare.Persistence.Mapping
{
    /// <summary>
    /// Represents the medication request type entity configuration.
    /// </summary>
    public class MedicationRequestTypeEntityConfiguration : IEntityTypeConfiguration<MedicationRequestType>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<MedicationRequestType> builder)
        {
            builder.ToTable("Medication_Request_Types");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasData(
                new MedicationRequestType
                {
                    Id = 1,
                    Name = "Just Once",
                    Description = "Book for only one time.",
                    IsActive = true,
                },
                new MedicationRequestType
                {
                    Id = 2,
                    Name = "Weekly",
                    Description = "Automatically book the same medication request every week.",
                    IsActive = true,
                });
        }
    }
}
