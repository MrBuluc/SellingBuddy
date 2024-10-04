using WebApiGateway.Application.Models.Basket;

namespace WebApiGateway.Application.Interfaces.Basket
{
    public interface IBasketService
    {
        Task<BasketData> GetMyBasket();
        Task<BasketData?> UpdateAsync(BasketData basket);
    }
}
