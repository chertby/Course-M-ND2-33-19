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
            var dbSet = dbContext.News;
            var result = await dbSet
                .AsNoTracking()
                .Include(x => x.Category)
                .ToListAsync();

            return result;
        }
    }
}
