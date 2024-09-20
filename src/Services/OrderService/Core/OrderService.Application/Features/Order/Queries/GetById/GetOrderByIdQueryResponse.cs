using OrderService.Application.DTOs;

namespace OrderService.Application.Features.Order.Queries.GetById
{
    public class GetOrderByIdQueryResponse
    {
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public AddressDTO Address { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
