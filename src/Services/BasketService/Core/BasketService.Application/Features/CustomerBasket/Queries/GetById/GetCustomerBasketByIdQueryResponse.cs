using BasketService.Application.DTOs;

namespace BasketService.Application.Features.CustomerBasket.Queries.GetById
{
    public class GetCustomerBasketByIdQueryResponse
    {
        public CustomerBasketDTO CustomerBasket { get; set; }
    }
}
