using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PinkPoint.Core.Repositories.Base;
using PinkPoint.DataAccess.Helpers;

namespace PinkPoint.Infrastructure.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DataContext _Context;
        private DbSet<TEntity> _dbSet;

        public Repository(DataContext context)
        {
            this._Context = context;
            this._dbSet = _Context.Set<TEntity>();
        }
        public async Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
           return await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIDAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<EntityEntry<TEntity>> RemoveAsync(Guid id)
        {
            return _dbSet.Remove(await GetByIDAsync(id));
        }

        public Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public Task update(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }


    }
}
