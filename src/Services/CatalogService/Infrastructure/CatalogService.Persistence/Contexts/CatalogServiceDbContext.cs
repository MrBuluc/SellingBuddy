using CatalogService.Domain.Common;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace CatalogService.Persistence.Contexts
{
    public class CatalogServiceDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "catalog";

        public DbSet<Item> Items { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Domain.Entities.Type> Types { get; set; }

        public CatalogServiceDbContext(DbContextOptions<CatalogServiceDbContext> options) : base(options) { }
        public CatalogServiceDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (EntityEntry<EntityBase> entry in ChangeTracker.Entries<EntityBase>())
            {
                _ = entry.State switch
                {
                    EntityState.Added => entry.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => entry.Entity.UpdatedDate = DateTime.Now,
                    EntityState.Deleted => entry.Entity.DeletedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
