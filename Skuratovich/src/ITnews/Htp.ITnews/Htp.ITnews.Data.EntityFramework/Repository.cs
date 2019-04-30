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

        protected DbSet<TEntity> dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public Task<TEntity> EditAsync(TEntity entity)
        {
            dbSet.Update(entity);
            return Task.FromResult(entity);
        }

        public bool EntityExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await dbSet.ToListAsync();
            return result;
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            var result = await dbSet.FindAsync(id);
            return result;
        }
    }
}
