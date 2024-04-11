using Microsoft.EntityFrameworkCore;
using RenoCare.Domain;
using RenoCare.Persistence.Mapping;

namespace RenoCare.Persistence
{
    /// <summary>
    /// Application identity context that provide connection and access to domain entities within the database.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new PatientTypeConfiguration());
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
