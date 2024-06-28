using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;

namespace RenoCare.Persistence.Mapping
{
    public class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Path).IsRequired().HasColumnType("text");
            builder.Property(x => x.Name).IsRequired().HasColumnType("text");

            builder.Property(x => x.IsThumbnail).IsRequired();

            builder.Property(x => x.DialysisUnitId).IsRequired();
        }
    }
}
