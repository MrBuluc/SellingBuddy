using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Persistence.Context;

namespace OrderService.Persistence.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("statuses", OrderDbContext.DEFAULT_SCHEMA);

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Id).HasDefaultValue(1).ValueGeneratedNever().IsRequired();

            builder.Property(s => s.Name).HasMaxLength(200).IsRequired();
        }
    }
}
