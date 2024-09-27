using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Persistence.Context;

namespace OrderService.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order", OrderDbContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.Ignore(o => o.DomainEvents);

            builder.OwnsOne(o => o.Address, a =>
            {
                a.WithOwner();
            });

            builder.Property<int>("statusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusId")
                .IsRequired();

            builder.Metadata.FindNavigation(nameof(Order.Items))?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(o => o.Buyer)
                .WithMany()
                .HasForeignKey(o => o.BuyerId);

            builder.HasOne(o => o.Status)
                .WithMany()
                .HasForeignKey("statusId");

            // Common Fields
            builder.Property(o => o.CreatedDate).IsRequired();
            builder.Property(o => o.UpdatedDate).IsRequired(false);
        }
    }
}
