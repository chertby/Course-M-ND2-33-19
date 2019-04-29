using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Data.Contracts;

namespace Htp.ITnews.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        private INewsRepository newsRepository;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public Dictionary<Type, object> Repositories
        {
            get { return repositories; }
            set { Repositories = value; }
        }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ITransaction BeginTransaction()
        {
            var transaction = new Transaction(dbContext.Database.BeginTransaction());
            return transaction;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            if (Repositories.Keys.Contains(typeof(TEntity)))
            {
                return Repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            IRepository<TEntity> repository = new Repository<TEntity>(dbContext);
            Repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public INewsRepository NewsRepository
        {
            get
            {
                if (newsRepository == null)
                {
                    newsRepository = new NewsRepository(dbContext);
                }

                return newsRepository;
            }
        }
    }
}
