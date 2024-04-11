using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RenoCare.Domain.Identity;
using RenoCare.Persistence.Identity.Mapping;

namespace RenoCare.Persistence.Identity
{
    /// <summary>
    /// Application identity context that provide connection and access to identity domain entities within the database.
    /// </summary>
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserTypeConfiguration());
            builder.ApplyConfiguration(new RoleTypeConfiguration());
            builder.ApplyConfiguration(new UserRoleMappingTypeConfiguration());
        }
    }
}
