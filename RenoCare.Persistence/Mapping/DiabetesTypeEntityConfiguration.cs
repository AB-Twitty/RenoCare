using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain;

namespace RenoCare.Persistence.Mapping
{
    /// <summary>
    /// Represents the diabetes type entity configuration.
    /// </summary>
    public class DiabetesTypeEntityConfiguration : IEntityTypeConfiguration<DiabetesType>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<DiabetesType> builder)
        {
            builder.ToTable("Diabetes_Types");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired();

            builder.HasData(
                new DiabetesType
                {
                    Id = 1,
                    Name = "Non-diabetic",
                    Description = "No diabetes and fall within the normal range of blood sugar levels."
                },
                new DiabetesType
                {
                    Id = 2,
                    Name = "Type 1 diabetes",
                    Description = "Type 1 diabetes is where the blood glucose (sugar) level is too high because the body can’t make a hormone called insulin. The body still breaks down the carbohydrate from food and drink and turns it into glucose. But when the glucose enters the bloodstream, there’s no insulin to allow it into the body’s cells. More and more glucose then builds up in the bloodstream, leading to high blood sugar levels."
                },
                new DiabetesType
                {
                    Id = 3,
                    Name = "Type 2 diabetes",
                    Description = "Type 2 diabetes is where the insulin the pancreas makes can’t work properly, or the pancreas can’t make enough insulin. This means the blood glucose (sugar) levels keep rising. Having type 2 diabetes without treatment means that high sugar levels in the blood can seriously damage parts of the body, including the eyes, heart and feet. These are called the complications of diabetes. But with the right treatment and care, the patient can live well with type 2 diabetes and reduce the risk of developing them."
                },
                new DiabetesType
                {
                    Id = 4,
                    Name = "Gestational diabetes",
                    Description = "Gestational diabetes is diabetes that can develop during pregnancy. It affects women who haven't been affected by diabetes before. It means she has high blood sugar and needs to take extra care of herself and her bump. This will include eating well and keeping active. It usually goes away again after giving birth. It is usually diagnosed from a blood test 24 to 28 weeks into pregnancy."
                },
                new DiabetesType
                {
                    Id = 5,
                    Name = "Monogenic diabetes",
                    Description = "Monogenic diabetes is a rare condition, different from both type 1 and type 2 diabetes. It’s caused by a mutation in a single gene. If a parent has this mutation, their children have a 50p per cent chance of inheriting it. In some cases of monogenic diabetes, the condition can be managed with specific tablets and doesn’t require insulin treatment."
                });
        }
    }
}
