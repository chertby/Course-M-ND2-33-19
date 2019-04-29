using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Htp.ITnews.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Htp.ITnews.Data.EntityFramework
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var dbSet = dbContext.Set<TEntity>();
            await dbSet.AddAsync(entity);
            return entity;
        }

        public Task<TEntity> EditAsync(TEntity entity)
        {
            var dbSet = dbContext.Set<TEntity>();
            dbSet.Update(entity);
            return Task.FromResult(entity);
        }

        public bool EntityExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var dbSet = dbContext.Set<TEntity>();
            var result = await dbSet.ToListAsync();
            return result;
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            var dbSet = dbContext.Set<TEntity>();
            var result = await dbSet.FindAsync(id);
            return result;
        }
    }
}
