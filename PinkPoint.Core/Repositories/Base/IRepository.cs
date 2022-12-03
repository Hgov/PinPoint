
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace PinkPoint.Core.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //Task<IQueryable<TEntity>> IncludeManyAsync(params Expression<Func<TEntity, object>>[] includes);
        //Task<IEnumerable<TEntity>> GetSqlAsync(string sql);
        Task<TEntity> GetByIDAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<EntityEntry<TEntity>> RemoveAsync(Guid id);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

    }
}
