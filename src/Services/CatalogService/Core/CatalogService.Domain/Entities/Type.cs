using CatalogService.Domain.Common;

namespace CatalogService.Domain.Entities
{
    public class Type : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
