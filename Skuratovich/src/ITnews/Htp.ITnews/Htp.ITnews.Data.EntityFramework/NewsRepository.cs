﻿using System;
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
            var newsTags = dbContext.NewsTags;
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

            var newsTags = dbContext.NewsTags;
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
            var newsTags = dbContext.NewsTags;
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

        public async Task RateAsync(News news, AppUser user, int value)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var ratings = dbContext.Ratings;

            var rate = await ratings.FirstOrDefaultAsync(l => l.NewsId.Equals(news.Id) && l.AppUserId.Equals(user.Id));
            if (rate != null)
            {
                ratings.Remove(rate);
                news.RatingSum -= rate.Value;
                --news.RatingCount;
                CountRating(news);
            }
            if (value > 0)
            {
                rate = new Rating { NewsId = news.Id, AppUserId = user.Id, Value = value };
                await ratings.AddAsync(rate);
                news.RatingSum += rate.Value;
                ++news.RatingCount;
                CountRating(news);
            }
        }

        public async Task<int> GetRatingAsync(News news, AppUser user)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var ratings = dbContext.Ratings;

            var rate = await ratings.FirstOrDefaultAsync(l => l.NewsId.Equals(news.Id) && l.AppUserId.Equals(user.Id));
            return rate == null ? 0 : rate.Value;
        }

        private void CountRating(News news)
        {
            if (news.RatingCount > 0)
            {
                news.Rating = (decimal) news.RatingSum / news.RatingCount;
            }
            else
            {
                news.Rating = 0;
            }
        }

    }
}
