using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PinPoint.Core.Repositories.Base;
using PinPoint.DataAccess.Helpers;

namespace PinPoint.Infrastructure.Repositories.Base
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
        public void Remove(TEntity entity)
        {
             _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

<<<<<<< HEAD
        public async Task<TEntity> UpdateAsync(TEntity entity)
=======
<<<<<<< Updated upstream
        public Task UpdateAsync(TEntity entity)
=======
        public TEntity Update(TEntity entity)
>>>>>>> Stashed changes
>>>>>>> cec7eb19645063b06087ae3eac9d2594120c7975
        {
            return _dbSet.Update(entity).Entity;
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
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
