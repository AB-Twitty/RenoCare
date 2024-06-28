using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;

namespace RenoCare.Persistence.Mapping
{
    public class VirusEntityConfiguration : IEntityTypeConfiguration<Virus>
    {
        public void Configure(EntityTypeBuilder<Virus> builder)
        {
            builder.ToTable("Viruses");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Abbreviation).IsRequired();
            builder.Property(x => x.Description).IsRequired();

            builder.HasMany(x => x.Patients).WithMany(p => p.Viruses)
                .UsingEntity(t => t.ToTable("Patients_With_Viruses_Mapping"));

            builder.HasMany(x => x.DialysisUnits).WithMany(d => d.AcceptingViruses)
                .UsingEntity(t => t.ToTable("DialysisUnits_Accepting_Viruses_Mapping"));

            builder.HasData(
                new Virus
                {
                    Id = 1,
                    Name = "Human Immunodeficiency Virus",
                    Abbreviation = "HIV",
                    Description = "HIV attacks the body’s immune system, specifically the CD4 cells (T cells), which help the immune system fight off infections. If left untreated, HIV reduces the number of these cells, making the body more vulnerable to infections and certain cancers."
                },
                new Virus
                {
                    Id = 2,
                    Name = "Hepatitis B Virus",
                    Abbreviation = "HBV",
                    Description = "HBV is a virus that infects the liver, causing inflammation and potentially leading to serious conditions such as liver cirrhosis or liver cancer. It is transmitted through contact with infectious body fluids, such as blood, semen, and vaginal fluids."
                },
                new Virus
                {
                    Id = 3,
                    Name = "Hepatitis C Virus",
                    Abbreviation = "HCV",
                    Description = "HCV is a liver infection caused by the hepatitis C virus. It can lead to chronic liver disease, including cirrhosis and liver cancer. HCV is primarily spread through contact with blood from an infected person."
                }
                );
        }
    }
}
