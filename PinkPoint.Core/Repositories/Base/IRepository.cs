
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PinkPoint.Core.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIDAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<EntityEntry<TEntity>> RemoveAsync(Guid id);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        Task update(TEntity entity);

    }
}
