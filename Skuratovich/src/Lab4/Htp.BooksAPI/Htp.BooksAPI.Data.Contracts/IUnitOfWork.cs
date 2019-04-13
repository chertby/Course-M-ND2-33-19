﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Htp.BooksAPI.Data.Contracts
{
    public interface IUnitOfWork
    {
        //IEnumerable<TEntity> GetAll<TKey, TEntity>();
        //IEnumerable<TEntity> FindByCondition<TKey, TEntity>(Expression<Func<TEntity, bool>> expression);

        //TEntity Get<Tkey, TEntity>(Tkey id);
        //void Add<TKey, TEntity>(TEntity entity);
        //void Update<TKey, TEntity>(TEntity entity);
        //void Delete<Tkey, TEntity>(TEntity entity);
        

        IBookRepository BookRepository { get; }

        IAppUserRepository AppUserRepository { get; }

        IRepository<T> Repository<T>() where T : class;

        void SaveChanges();
        Task<int> SaveChangesAsync();
        ITransaction BeginTransaction();
    }
}
