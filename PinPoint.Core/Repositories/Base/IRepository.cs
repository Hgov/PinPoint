using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PinPoint.Core.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //Task<IQueryable<TEntity>> IncludeManyAsync(params Expression<Func<TEntity, object>>[] includes);
        //Task<IEnumerable<TEntity>> GetSqlAsync(string sql);
        Task<TEntity> GetByIDAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> RemoveAsync(Guid id);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

    }
}
