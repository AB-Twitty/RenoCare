using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;
using System;

namespace RenoCare.Persistence.Mapping
{
    /// <summary>
    /// Represents the dialysis treatment session report entity configuration.
    /// </summary>
    public class ReportEntityConfiguration : IEntityTypeConfiguration<Report>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Session_Reports");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.LastModifiedDate).IsRequired().ValueGeneratedOnAddOrUpdate();

            builder.Property(x => x.PatientId).IsRequired();
            builder.HasOne(p => p.Patient).WithOne().HasForeignKey<Report>(p => p.PatientId);

            builder.Property(x => x.DialysisUnitId).IsRequired();
            builder.HasOne(p => p.DialysisUnit).WithOne().HasForeignKey<Report>(p => p.DialysisUnitId);

            builder.Property(x => x.MedicationRequestId).IsRequired();
            builder.HasOne(p => p.MedicationRequest).WithOne().HasForeignKey<Report>(p => p.MedicationRequestId);

            //*****************************Session Details******************************
            builder.Property(x => x.Nephrologist).IsRequired().HasMaxLength(80);

            builder.Property(x => x.DialysisDuration).IsRequired();

            builder.Property(x => x.DialysisFrequency);

            builder.Property(x => x.VascularAccessType).IsRequired().HasColumnType("nvarchar(50)")
                .HasConversion(v => v.ToString(),
                    v => (VascularType)Enum.Parse(typeof(VascularType), v));

            builder.Property(x => x.DialyzerType).IsRequired().HasColumnType("nvarchar(50)")
                .HasConversion(v => v.ToString(),
                    v => (DialyzerType)Enum.Parse(typeof(DialyzerType), v));


            //*****************************Vital Signs******************************
            builder.Property(x => x.PreWeight).IsRequired();

            builder.Property(x => x.PreWeight).IsRequired();

            builder.Property(x => x.PreSystolicBloodPressure).IsRequired().HasMaxLength(10);
            builder.Property(x => x.DuringSystolicBloodPressure).IsRequired().HasMaxLength(10);
            builder.Property(x => x.PostSystolicBloodPressure).IsRequired().HasMaxLength(10);

            builder.Property(x => x.PreDiastolicBloodPressure).IsRequired().HasMaxLength(10);
            builder.Property(x => x.DuringDiastolicBloodPressure).IsRequired().HasMaxLength(10);
            builder.Property(x => x.PostDiastolicBloodPressure).IsRequired().HasMaxLength(10);

            builder.Property(x => x.DryWeight).IsRequired();

            builder.Property(x => x.HeartRate).IsRequired();


            //*****************************Dialysis Treatment Outcomes******************************
            builder.Property(x => x.PreUrea).IsRequired();

            builder.Property(x => x.PostUrea).IsRequired();

            builder.Property(x => x.UreaReductionRatio).IsRequired();

            builder.Property(x => x.TotalFluidRemoval).IsRequired();

            builder.Property(x => x.FluidRemovalRate).IsRequired();

            builder.Property(x => x.UrineOutput).IsRequired();

            builder.Property(x => x.Kt_V).IsRequired();


            //*****************************Para-Clinical Examinations******************************
            builder.Property(x => x.Creatinine).IsRequired();

            builder.Property(x => x.Potassium).IsRequired();

            builder.Property(x => x.Hemoglobin).IsRequired();

            builder.Property(x => x.Hematocrit).IsRequired();

            builder.Property(x => x.Albumin).IsRequired();
        }
    }
}
