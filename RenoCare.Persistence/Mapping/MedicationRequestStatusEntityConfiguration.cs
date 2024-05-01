using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;

namespace RenoCare.Persistence.Mapping
{
    /// <summary>
    /// Represents the medication request status entity configuration.
    /// </summary>
    public class MedicationRequestStatusEntityConfiguration : IEntityTypeConfiguration<MedicationRequestStatus>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<MedicationRequestStatus> builder)
        {
            builder.ToTable("Medication_Request_Status");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.LabelClass).IsRequired(false);

            builder.HasData(
                new MedicationRequestStatus
                {
                    Id = 1,
                    Name = "Pending",
                    Description = "Indicates that the medication request is pending / awaiting to be reviewed by the healthcare provider.",
                    LabelClass = "#f0ad4e"
                },
                new MedicationRequestStatus
                {
                    Id = 2,
                    Name = "Upcoming",
                    Description = "Indicates that the medication request is upcoming / reviewed by the healthcare provider and approved it.",
                    LabelClass = "#20809D"
                },
                new MedicationRequestStatus
                {
                    Id = 3,
                    Name = "Completed",
                    Description = "Indicates that the medication request is completed.",
                    LabelClass = "#5cb85c"
                },
                new MedicationRequestStatus
                {
                    Id = 4,
                    Name = "Rejected",
                    Description = "Indicates that the medication request is rejected / reviewed by the healthcare provider and declined it.",
                    LabelClass = "#A72925"
                },
                new MedicationRequestStatus
                {
                    Id = 5,
                    Name = "Cancelled",
                    Description = "Indicates that the medication request is either cancelled by the patient or its time has passed without reviewing it.",
                    LabelClass = "#d9534f"
                });
        }
    }
}
