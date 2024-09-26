using MediatR;
using OrderService.Application.DTOs;

namespace OrderService.Application.Features.Order.Commands.Create
{
    public class CreateOrderCommandRequest : IRequest<bool>
    {
        public string UserName { get; set; }
        public AddressDTO Address { get; set; }
        public CardDTO Card { get; set; }

        public IEnumerable<OrderItemDTO> OrderItems { get; set; } = [];
    }
}
