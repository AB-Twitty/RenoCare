using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenoCare.Domain.Chat;

namespace RenoCare.Persistence.Mapping
{
    /// <summary>
    /// Represents the chat message entity configuration.
    /// </summary>
    public class ChatMessageEntityConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        /// <summary>
        /// Configure entity options.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an entity.</param>
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable("Messages");
            builder.HasKey(x => x.Id);


            builder.Property(p => p.SenderId).IsRequired();
            builder.HasOne(p => p.Sender).WithOne().HasForeignKey<ChatMessage>(p => p.SenderId);


            builder.Property(p => p.ReceiverId).IsRequired();
            builder.HasOne(p => p.Receiver).WithOne().HasForeignKey<ChatMessage>(p => p.ReceiverId);

            builder.Property(p => p.Message).IsRequired().HasColumnType("text");

            builder.Property(p => p.SendingTime).IsRequired();
            builder.Property(p => p.IsRead).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
