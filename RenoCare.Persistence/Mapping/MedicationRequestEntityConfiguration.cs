﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;

namespace RenoCare.Persistence.Mapping
{
    /// <summary>
    /// Represents the medication request entity configuration.
    /// </summary>
    public class MedicationRequestEntityConfiguration : IEntityTypeConfiguration<MedicationRequest>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<MedicationRequest> builder)
        {
            builder.ToTable("Medication_Requests");
            builder.HasKey(x => x.Id);

            builder.Property(p => p.PatientProblem).IsRequired(false).HasColumnType("text");

            builder.Property(p => p.AppointmentDate).IsRequired();

            builder.HasOne(p => p.Session).WithMany(s => s.MedReqs).HasForeignKey(p => p.SessionId).IsRequired();


            builder.Property(p => p.PatientId).IsRequired();
            builder.HasOne(p => p.Patient).WithOne().HasForeignKey<MedicationRequest>(p => p.PatientId);


            builder.Property(p => p.StatusId).IsRequired();
            builder.HasOne(p => p.Status).WithOne().HasForeignKey<MedicationRequest>(p => p.StatusId);


            builder.Property(p => p.TypeId).IsRequired();
            builder.HasOne(p => p.Type).WithOne().HasForeignKey<MedicationRequest>(p => p.TypeId);

            builder.Property(p => p.ReportId).IsRequired(false);
            builder.HasOne(p => p.Report).WithOne(r => r.MedicationRequest).HasForeignKey<MedicationRequest>(p => p.ReportId).IsRequired(false);

            builder.Property(p => p.DialysisUnitId).IsRequired();
            builder.HasOne(p => p.DialysisUnit).WithMany(d => d.MedRequests).HasForeignKey(p => p.DialysisUnitId);

            builder.Property(p => p.Treatment)
                .IsRequired();
        }
    }
}
