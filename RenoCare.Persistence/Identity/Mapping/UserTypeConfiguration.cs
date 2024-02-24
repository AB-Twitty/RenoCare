using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain.Identity;

namespace RenoCare.Persistence.Identity.Mapping
{
    /// <summary>
    /// Represents the user entity configuration.
    /// </summary>
    public class UserTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var hasher = new PasswordHasher<AppUser>();

            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(25);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(25);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);

            builder.HasData(
                new AppUser
                {
                    Id = "a6d6f491-1957-4e70-98c7-997eb0d3256f",
                    FirstName = "System",
                    LastName = "Admin",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    UserName = "admin@localhost.com",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    PasswordHash = hasher.HashPassword(null, "123456"),
                },
                new AppUser
                {
                    Id = "a6d6f491-1957-4e70-98c7-997eb0d3255f",
                    FirstName = "Abdelrahman",
                    LastName = "Fawzy",
                    Email = "bobofawzy3@gmail.com",
                    NormalizedEmail = "BOBOFAWZY3@GMAIL.COM",
                    UserName = "bobofawzy3@gmail.com",
                    NormalizedUserName = "BOBOFAWZY3@GMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "123456"),
                }
                );
        }
    }
}
