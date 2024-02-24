using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RenoCare.Persistence.Identity.Mapping
{
    /// <summary>
    /// Represents the role entity configuration.
    /// </summary>
    public class RoleTypeConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "5eb1897c-7ba1-4595-b37c-f48bcd61e035",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "5eb1897c-7ba1-4595-b37c-f48bcd61e034",
                    Name = "Patient",
                    NormalizedName = "PATIENT"
                },
                new IdentityRole
                {
                    Id = "5eb1897c-7ba1-4595-b37c-f48bcd61e033",
                    Name = "HealthCare",
                    NormalizedName = "HEALTHCARE"
                }
                );
        }
    }
}
