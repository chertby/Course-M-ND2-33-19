using System;
using System.Collections.Generic;
using System.Linq;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Data.Contracts.Entities;

namespace Htp.ITnews.Data.EntityFramework
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Tag> GetTagsByTerm(string term)
        {
            var result = dbSet.Where(t => t.Title.Contains(term));
            return result;
        }
    }
}
