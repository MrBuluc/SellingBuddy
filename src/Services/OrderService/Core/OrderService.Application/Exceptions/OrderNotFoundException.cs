using SendGrid.Helpers.Errors.Model;

namespace OrderService.Application.Exceptions
{
    public class OrderNotFoundException() : NotFoundException("Order Not Found!") { }
}
