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
            builder.ApplyConfiguration(new HypertensionTypeConfiguration());
            builder.ApplyConfiguration(new DiabetesTypeConfiguration());
            builder.ApplyConfiguration(new SmokingStatusTypeConfiguration());
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<HypertensionType> HypertensionTypes { get; set; }
        public DbSet<DiabetesType> DiabetesTypes { get; set; }
        public DbSet<SmokingStatus> SmokingStatus { get; set; }
    }
}
