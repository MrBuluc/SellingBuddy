using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Persistence.Context;

namespace OrderService.Persistence.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Item", OrderDbContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();

            builder.Ignore(i => i.DomainEvents);

            //builder.Property<int>("OrderId").IsRequired();

            builder.OwnsOne(i => i.Product, p =>
            {
                p.WithOwner();

                p.Property(pro => pro.UnitPrice).IsRequired(true).HasColumnType("decimal(10, 2)");
            });

            // Common Fields
            builder.Property(i => i.CreatedDate).IsRequired();
            builder.Property(i => i.UpdatedDate).IsRequired(false);
        }
    }
}
