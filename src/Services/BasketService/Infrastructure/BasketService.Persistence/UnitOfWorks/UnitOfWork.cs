using BasketService.Application.Interfaces.Repositories;
using BasketService.Application.Interfaces.UnitOfWorks;
using BasketService.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace BasketService.Persistence.UnitOfWorks
{
    public class UnitOfWork(ConnectionMultiplexer redis, ILoggerFactory loggerFactory) : IUnitOfWork
    {
        private readonly ConnectionMultiplexer redis = redis;
        private readonly ILoggerFactory loggerFactory = loggerFactory;

        IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(redis);

        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(loggerFactory, redis);
    }
}
