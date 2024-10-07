using WebApp.Application.Services.DTOs;
using WebApp.Domain.Models.ViewModels;

namespace WebApp.Application.Services.Interfaces
{
    public interface IOrderService
    {
        BasketDTO MapOrderToBasket(Order order);
    }
}
