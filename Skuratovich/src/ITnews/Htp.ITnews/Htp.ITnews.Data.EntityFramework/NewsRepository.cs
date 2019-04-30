using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Htp.ITnews.Data.EntityFramework
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public new async Task<IEnumerable<News>> GetAllAsync()
        {
            var result = await dbSet
                .AsNoTracking()
                .Include(x => x.Category)
                .ToListAsync();

            return result;
        }

        public new async Task<News> GetAsync(Guid id)
        {
            var result = await dbSet.FindAsync(id);

            await dbContext.Entry(result)
                .Reference(b => b.Category)
                .LoadAsync();

            //await dbContext.Entry(book)
            //.Reference(b => b.UpdatedBy)
            //.LoadAsync();

            return result;
        }
    }
}
