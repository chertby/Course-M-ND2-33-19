using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Data.Contracts.Extensions;
using Htp.ITnews.Data.EntityFramework.Extensions;
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
                .Include(x => x.Author)
                .Include(x => x.UpdatedBy)
                .ToListAsync();

            return result;
        }

        public IQueryable<News> GetAll()
        {
            var result = dbSet
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Include(x => x.UpdatedBy);

            return result;
        }

        public async Task<IList<string>> GetTagsAsync(News news)
        {
            var newsTags = dbContext.NewsTag;
            var tags = dbContext.Tags;

            var newsId = news.Id;

            var query = from newsTag in newsTags
                        where newsTag.NewsId.Equals(newsId)
                        join tag in tags on newsTag.TagId equals tag.Id
                        select tag.Title;

            return await query.ToListAsync();
        }

        public async Task AddToTagAsync(News news, string tagName)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news));
            }
            if (string.IsNullOrWhiteSpace(tagName))
            {
                throw new ArgumentException("Value cannot be null or empty. Null or empty error message.", nameof(tagName));
            }

            var newsTags = dbContext.NewsTag;
            var tags = dbContext.Tags;

            var tagEntity = await tags.SingleOrDefaultAsync(t => t.Title.ToUpper() == tagName.ToUpper());
            if (tagEntity == null)
            {
                tagEntity = new Tag { Title = tagName };
                tags.Add(tagEntity);
            }

            var nt = new NewsTag { NewsId = news.Id, TagId = tagEntity.Id };
            await newsTags.AddAsync(nt);
        }

        public async Task RemoveFromTagAsync(News news, string tagName)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news));
            }
            if (string.IsNullOrWhiteSpace(tagName))
            {
                throw new ArgumentException("Value cannot be null or empty. Null or empty error message.", nameof(tagName));
            }
            var newsTags = dbContext.NewsTag;
            var tags = dbContext.Tags;

            var tagEntity = await tags.SingleOrDefaultAsync(t => t.Title.ToUpper() == tagName.ToUpper());
            if (tagEntity != null)
            {
                var tagId = tagEntity.Id;
                var newsId = news.Id;
                var newsTag = await newsTags.FirstOrDefaultAsync(t => tagId.Equals(t.TagId) && t.NewsId.Equals(newsId));
                if (newsTag != null)
                {
                    newsTags.Remove(newsTag);
                }
            }
        }

        //public void Test(News news)
        //{
        //    var newsTags = dbContext.NewsTag;
        //    var tags = dbContext.Tags;

        //    var newsId = news.Id;

        //    var query = from newsTag in newsTags
        //                where newsTag.NewsId.Equals(newsId)
        //                join tag in tags on newsTag.TagId equals tag.Id
        //                select tag.Title;

        //    //return await query.ToListAsync().WithCurrentCulture();

        //    //var result = newsTags
        //    //.Where(x => x.NewsId == newsId)
        //    //.Join(tags, n => n.TagId, t => t.Id, (n, t) => t.Title);

        //    //var result = await newsTags.FindAsync(newsId);

        //    //await dbContext.Entry(result)
        //    //.Reference(n => n.)
        //    //.LoadAsync();


        //    //result.Select();

        //    //newsTags.Where(

        //    //var result = dbSet.Where(t => t.Title.Contains(term)).Select(t => t.Title);

        //}
    }
}
