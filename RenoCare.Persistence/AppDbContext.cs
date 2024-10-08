﻿using Microsoft.EntityFrameworkCore;
using RenoCare.Domain;
using RenoCare.Domain.Chat;
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
            builder.ApplyConfiguration(new DialysisUnitEntityConfiguration());
            builder.ApplyConfiguration(new ChatMessageEntityConfiguration());
            builder.ApplyConfiguration(new SessionTimetableEntityConfiguration());
            builder.ApplyConfiguration(new ImageEntityConfiguration());
            builder.ApplyConfiguration(new ReviewEntityConfiguration());
            builder.ApplyConfiguration(new VirusEntityConfiguration());
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<HypertensionType> HypertensionTypes { get; set; }
        public DbSet<DiabetesType> DiabetesTypes { get; set; }
        public DbSet<SmokingStatus> SmokingStatus { get; set; }
        public DbSet<MedicationRequest> MedicationRequests { get; set; }
        public DbSet<MedicationRequestStatus> MedicationRequestStatus { get; set; }
        public DbSet<MedicationRequestType> MedicationRequestTypes { get; set; }
        public DbSet<DialysisUnit> DialysisUnits { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }
        public DbSet<SessionTimetable> Sessions { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Virus> Viruses { get; set; }
    }
}
