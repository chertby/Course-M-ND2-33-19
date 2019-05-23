using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Htp.ITnews.Data.Contracts.Extensions;

namespace Htp.ITnews.Data.Contracts
{
    public interface IRepository<TEntity>
    {
        bool EntityExists(Guid id);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> GetAsync(Guid id, Func<IIncludable<TEntity>, IIncludable> includes);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> EditAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
