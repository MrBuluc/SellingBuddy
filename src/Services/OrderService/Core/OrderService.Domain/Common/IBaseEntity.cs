namespace OrderService.Domain.Common
{
    public interface IBaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
