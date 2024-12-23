﻿using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Common;

namespace CatalogService.Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : class, IEntityBase, new();
        IWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityBase, new();
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
