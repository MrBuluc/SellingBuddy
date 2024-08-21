namespace CatalogService.Application.Exceptions
{
    public class ItemNotFoundException(int id) : Exception($"There is no item with id: {id}") { }
}
