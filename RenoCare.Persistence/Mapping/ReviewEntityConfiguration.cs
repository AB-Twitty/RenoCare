using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;

namespace RenoCare.Persistence.Mapping
{
    public class ReviewEntityConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PatientId).IsRequired();
            builder.HasOne(r => r.Patient).WithMany().HasForeignKey(r => r.PatientId);

            builder.Property(x => x.DialysisUnitId).IsRequired();

            builder.Property(x => x.Rating).IsRequired();

            builder.Property(x => x.Comment).IsRequired(false).HasColumnType("nvarchar(MAX)");
            builder.Property(x => x.CreationDate).IsRequired();
        }
    }
}
