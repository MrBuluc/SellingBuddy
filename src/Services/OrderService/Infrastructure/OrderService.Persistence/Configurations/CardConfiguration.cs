using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using OrderService.Persistence.Context;

namespace OrderService.Persistence.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("cards", OrderDbContext.DEFAULT_SCHEMA);

            builder.Ignore(c => c.DomainEvents);

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();

            builder.Property<int>("BuyerId").IsRequired();

            builder.Property(c => c.HolderName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("HolderName")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.Number)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Number")
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(c => c.Expiration)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Expiration")
                .HasMaxLength(25)
                .IsRequired();

            // Common Fields
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired(false);
        }
    }
}
