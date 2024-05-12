using Microsoft.EntityFrameworkCore;
using RenoCare.Domain;
using RenoCare.Domain.Identity;
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

            builder.ApplyConfiguration(new PatientEntityConfiguration());
            builder.ApplyConfiguration(new HypertensionTypeEntityConfiguration());
            builder.ApplyConfiguration(new DiabetesTypeEntityConfiguration());
            builder.ApplyConfiguration(new SmokingStatusEntityConfiguration());
            builder.ApplyConfiguration(new MedicationRequestEntityConfiguration());
            builder.ApplyConfiguration(new MedicationRequestStatusEntityConfiguration());
            builder.ApplyConfiguration(new MedicationRequestTypeEntityConfiguration());
            builder.ApplyConfiguration(new ReportEntityConfiguration());
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<HypertensionType> HypertensionTypes { get; set; }
        public DbSet<DiabetesType> DiabetesTypes { get; set; }
        public DbSet<SmokingStatus> SmokingStatus { get; set; }
        public DbSet<MedicationRequest> MedicationRequests { get; set; }
        public DbSet<MedicationRequestStatus> MedicationRequestStatus { get; set; }
        public DbSet<MedicationRequestType> MedicationRequestTypes { get; set; }

        public DbSet<Report> Reports { get; set; }
    }
}
