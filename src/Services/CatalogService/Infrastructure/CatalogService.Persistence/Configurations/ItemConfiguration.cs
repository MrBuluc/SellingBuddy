using CatalogService.Domain.Entities;
using CatalogService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Persistence.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Catalog", CatalogServiceDbContext.DEFAULT_SCHEMA);

            builder.Property(i => i.Id).UseHiLo("catalog_hilo").IsRequired();

            builder.Property(i => i.Name).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Price).IsRequired(true);
            builder.Property(i => i.PictureFileName).IsRequired(false);
            builder.Ignore(i => i.PictureUri);

            builder.HasOne(i => i.Brand).WithMany().HasForeignKey(i => i.BrandId);
            builder.HasOne(i => i.Type).WithMany().HasForeignKey(i => i.TypeId);

            // Common Fields
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired(false);
            builder.Property(x => x.UpdatedBy).IsRequired(false);
            builder.Property(x => x.DeletedDate).IsRequired(false);
            builder.Property(x => x.DeletedBy).IsRequired(false);
        }
    }
}
