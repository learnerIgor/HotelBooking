﻿using Booking.Application.Abstractions.Persistence.Repositories.Read;

namespace Booking.Application.Abstractions.Persistence.Repositories.Write
{
    public interface IBaseWriteRepository<TEntity> : IBaseReadRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        Task<int> RemoveAsync(TEntity entity, CancellationToken cancellationToken);
        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    }
}
