namespace WebApiGateway.Application.Exceptions
{
    public class CatalogItemNotFoundException() : Exception("Catalog Item not found!") { }

    public class BasketNotFoundException() : Exception("Basket not found!") { }
}
