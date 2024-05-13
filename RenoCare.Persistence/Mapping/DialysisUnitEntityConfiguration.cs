using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;

namespace RenoCare.Persistence.Mapping
{
    /// <summary>
    /// Represents the dialysis unit entity configuration.
    /// </summary>
    public class DialysisUnitEntityConfiguration : IEntityTypeConfiguration<DialysisUnit>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<DialysisUnit> builder)
        {
            builder.ToTable("Dialysis_Units");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasColumnType("text");
            builder.Property(x => x.PhoneNumber).IsRequired(true).HasMaxLength(15);

            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.Country).IsRequired().HasMaxLength(50);
            builder.Property(x => x.City).IsRequired().HasMaxLength(50);

            builder.Property(x => x.IsHDSupported).IsRequired();
            builder.Property(x => x.IsHDFSupported).IsRequired();

            builder.Property(p => p.UserId);
            builder.HasOne(p => p.User).WithOne().HasForeignKey<DialysisUnit>(p => p.UserId);

            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.HasData(
                new DialysisUnit
                {
                    Id = 1,
                    UserId = "30aaf317-be57-4870-9768-2af3599936v2",
                    Name = "Dialysis unit name",
                    Description = "this is the description for the dialysis unit",
                    Country = "France",
                    City = "Paris",
                    Address = "the street where the unit is located",
                    IsHDSupported = true,
                    IsHDFSupported = true,
                    PhoneNumber = "123456789"
                });
        }
    }
}
