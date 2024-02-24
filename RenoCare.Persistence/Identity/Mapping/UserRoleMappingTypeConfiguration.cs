using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RenoCare.Persistence.Identity.Mapping
{
    /// <summary>
    /// Represents the user role mapping entity configuration.
    /// </summary>
    public class UserRoleMappingTypeConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "a6d6f491-1957-4e70-98c7-997eb0d3256f",
                    RoleId = "5eb1897c-7ba1-4595-b37c-f48bcd61e035"
                },
                new IdentityUserRole<string>
                {
                    UserId = "a6d6f491-1957-4e70-98c7-997eb0d3255f",
                    RoleId = "5eb1897c-7ba1-4595-b37c-f48bcd61e034"
                }
                );
        }
    }
}
