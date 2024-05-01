using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;

namespace RenoCare.Persistence.Mapping
{
    /// <summary>
    /// Represents the smoking status entity configuration.
    /// </summary>
    public class SmokingStatusEntityConfiguration : IEntityTypeConfiguration<SmokingStatus>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<SmokingStatus> builder)
        {
            builder.ToTable("Smoking_Status");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired();

            builder.HasData(
                new SmokingStatus
                {
                    Id = 1,
                    Name = "Non Smoker",
                    Description = "Individuals who have never smoked."
                },
                new SmokingStatus
                {
                    Id = 2,
                    Name = "Former Smoker",
                    Description = "Individuals who used to smoke but have successfully quit."
                },
                new SmokingStatus
                {
                    Id = 3,
                    Name = "Current Smoker",
                    Description = "Individuals who currently smoke."
                });
        }
    }
}
