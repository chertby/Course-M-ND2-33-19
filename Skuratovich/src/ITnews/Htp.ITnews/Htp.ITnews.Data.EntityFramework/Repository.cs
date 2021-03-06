﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Data.Contracts.Extensions;
using Htp.ITnews.Data.EntityFramework.Extensions;
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

        public IQueryable<TEntity> GetAll(Func<IIncludable<TEntity>, IIncludable> includes)
        {
            var result = dbSet
                .IncludeMultiple(includes);

            return result;
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            var result = dbSet
                .Where(expression);
            return result;
        }

        public IQueryable<TEntity> FindByCondition(
            Expression<Func<TEntity, bool>> expression, 
            Func<IIncludable<TEntity>, IIncludable> includes)
        {
            var result = dbSet
                .IncludeMultiple(includes)
                .Where(expression);
            return result;
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            var result = await dbSet.FindAsync(id);
            return result;
        }

        public async Task<TEntity> GetAsync(Guid id, Func<IIncludable<TEntity>, IIncludable> includes)
        {
            var result = await dbSet
                .IncludeMultiple(includes)
                .SingleOrDefaultAsync(n => n.Id == id);

            return result;
        }
    }
}
