
namespace BasketService.Domain.Common
{
    public class EntityBase<TKey>(TKey Id) : IEntityBase
    {
        public TKey Id { get; set; } = Id;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
