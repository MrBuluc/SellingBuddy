namespace BasketService.Domain.Common
{
    public interface IEntityBase
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
