using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Htp.ITnews.Data.Contracts
{
    public interface IRepository<TEntity>
    {
        bool EntityExists(Guid id);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> EditAsync(TEntity entity);
    }
}
