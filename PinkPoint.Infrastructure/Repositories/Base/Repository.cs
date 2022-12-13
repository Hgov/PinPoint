using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PinkPoint.Core.Repositories.Base;
using PinkPoint.DataAccess.Helpers;
using PinkPoint.Infrastructure.Repositories.Extensions;
using System.Linq.Expressions;

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
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var _entity = await _dbSet.AddAsync(entity);
            return _entity.Entity;
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
        public async Task<TEntity> RemoveAsync(Guid id)
        {
            var _entity = _dbSet.Remove(await GetByIDAsync(id));
            return _entity.Entity;
        }

        public Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }
        public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            return Task.CompletedTask;
        }




        //public async Task<IQueryable<TEntity>> IncludeManyAsync(params Expression<Func<TEntity, object>>[] includes)
        //{
        //    return await _dbSet.IncludeMultiple(includes);
        //}

        //public Task<IEnumerable<TEntity>> GetSqlAsync(string sql)
        //{
        //    return _dbSet.FromSql<TEntity>((FormattableString)sql);
        //}

    }
}
