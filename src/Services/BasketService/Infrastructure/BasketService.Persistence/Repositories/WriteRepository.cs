using BasketService.Application.Interfaces.Repositories;
using BasketService.Domain.Common;
using BasketService.Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BasketService.Persistence.Repositories
{
    public class WriteRepository<T>(ILoggerFactory loggerFactory, ConnectionMultiplexer redis) : IWriteRepository<T> where T : class, IEntityBase, new()
    {
        private readonly ILogger<WriteRepository<T>> logger = loggerFactory.CreateLogger<WriteRepository<T>>();
        private readonly IDatabase database = redis.GetDatabase();

        public async Task<bool> DeleteAsync(string id)
        {
            return await database.KeyDeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (typeof(T) != typeof(CustomerBasket))
            {
                return false;
            }

            CustomerBasket customerBasket = (entity as CustomerBasket)!;

            if (!(await database.StringSetAsync(customerBasket.BuyerId, JsonConvert.SerializeObject(customerBasket))))
            {
                logger.LogInformation("Problem occur persisting the item.");
                return false;
            }

            logger.LogInformation("Basket item persisted succesfully.");

            return true;
        }
    }
}
