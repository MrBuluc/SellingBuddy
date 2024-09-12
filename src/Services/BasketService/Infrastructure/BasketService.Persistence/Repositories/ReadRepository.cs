using BasketService.Application.Interfaces.Repositories;
using BasketService.Domain.Common;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BasketService.Persistence.Repositories
{
    public class ReadRepository<T>(ConnectionMultiplexer redis) : IReadRepository<T> where T : class, IEntityBase, new()
    {
        private readonly ConnectionMultiplexer redis = redis;
        private readonly IDatabase database = redis.GetDatabase();

        public async Task<T?> GetAsync(string id)
        {
            RedisValue data = await database.StringGetAsync(id);

            return data.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<T>(data!);
        }

        public IEnumerable<string>? GetUsers() => GetServer().Keys()?.Select(k => k.ToString());

        private IServer GetServer() => redis.GetServer(redis.GetEndPoints().First());
    }
}
