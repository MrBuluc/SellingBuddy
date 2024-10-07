using WebApp.Application.Services.DTOs;
using WebApp.Domain.Models.ViewModels;

namespace WebApp.Application.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Basket?> GetBasketAsync();
        Task<Basket?> UpdateBasketAsync(Basket basket);
        Task AddItemToBasketAsync(int productId);
        Task CheckoutAsync(BasketDTO basket);
    }
}
