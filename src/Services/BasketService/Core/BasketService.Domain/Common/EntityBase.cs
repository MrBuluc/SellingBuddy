
namespace BasketService.Domain.Common
{
    public class EntityBase<TKey> : IEntityBase
    {
        public TKey Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
