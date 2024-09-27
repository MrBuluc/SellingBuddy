using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using OrderService.Persistence.Context;

namespace OrderService.Persistence.Configurations
{
    public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.ToTable("Buyer", OrderDbContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Ignore(b => b.DomainEvents);

            builder.Property(b => b.Name).HasColumnType("name").HasColumnType("varchar").HasMaxLength(100);

            builder.HasMany(b => b.Cards)
                .WithOne()
                .HasForeignKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(Buyer.Cards))?.SetPropertyAccessMode(PropertyAccessMode.Field);

            // Common Fields
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired(false);
        }
    }
}
