using WebApiGateway.Application.Models.Basket;

namespace WebApiGateway.Application.Interfaces.Basket
{
    public interface IBasketService
    {
        Task<BasketData?> GetById(string id);
        Task<BasketData?> UpdateAsync(BasketData basket);
    }
}
