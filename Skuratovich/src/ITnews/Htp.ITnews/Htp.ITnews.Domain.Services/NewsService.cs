using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Data.EntityFramework.Extensions;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Htp.ITnews.Domain.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository newsRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public NewsService(INewsRepository newsRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.newsRepository = newsRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IQueryable<NewsViewModel> GetAll()
        {
            var news = newsRepository.GetAll(x => x
                .Include(n => n.Category)
                .Include(n => n.Author)
                .Include(n => n.UpdatedBy)
                .Include(n => n.NewsTags)
                    .ThenInclude(nt => nt.Tag));

            var result = news.ProjectTo<NewsViewModel>(mapper.ConfigurationProvider);
            return result;
        }

        public IQueryable<NewsViewModel> GetAllByTag(Guid tagId)
        {
            var news = newsRepository.FindByCondition(x => x.NewsTags.Any(nt => nt.TagId == tagId),
                x => x
                .Include(n => n.Category)
                .Include(n => n.Author)
                .Include(n => n.UpdatedBy)
                .Include(n => n.NewsTags)
                    .ThenInclude(nt => nt.Tag));

            var result = news.ProjectTo<NewsViewModel>(mapper.ConfigurationProvider);
            return result;
        }

        public async Task<NewsViewModel> GetAsync(Guid id)
        {
            var news = await newsRepository.GetAsync(id, x => x
                    .Include(n => n.Category)
                    .Include(n => n.Author)
                    .Include(n => n.UpdatedBy)
                    .Include(n => n.NewsTags)
                        .ThenInclude(nt => nt.Tag));

            var result = mapper.Map<NewsViewModel>(news);
            return result;
        }

        public async Task<NewsViewModel> GetAsync(Guid id, Guid userId)
        {
            var news = await newsRepository.GetAsync(id, x => x
                    .Include(n => n.Category)
                    .Include(n => n.Author)
                    .Include(n => n.UpdatedBy)
                    .Include(n => n.NewsTags)
                        .ThenInclude(nt => nt.Tag));

            var result = mapper.Map<NewsViewModel>(news);

            var user = await unitOfWork.Repository<AppUser>().GetAsync(userId);

            if (user != null)
            {
                var rating = await newsRepository.GetRatingAsync(news, user);
                result.Rating = rating;
            }

            return result;
        }

        public async Task<NewsViewModel> AddAsync(NewsViewModel newsViewModel)
        {
            var news = mapper.Map<News>(newsViewModel);

            using (var transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    news.Category = await unitOfWork.Repository<Category>().GetAsync(newsViewModel.CategoryId);
                    news.Author = await unitOfWork.Repository<AppUser>().GetAsync(newsViewModel.AuthorId);
                    news.Created = DateTime.Now;
                    await newsRepository.AddAsync(news);

                    await unitOfWork.SaveChangesAsync();

                    if (!string.IsNullOrEmpty(newsViewModel.StringTags))
                    {
                        var addedTags = newsViewModel.StringTags.Split(",");
                        await AddToTagsAsync(news.Id, addedTags);
                        await unitOfWork.SaveChangesAsync();
                    }

                    transaction.Commit();

                    newsViewModel = mapper.Map<NewsViewModel>(news);

                    return newsViewModel;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<NewsViewModel> EditAsync(NewsViewModel newsViewModel)
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var news = await newsRepository.GetAsync(newsViewModel.Id);
                    mapper.Map(newsViewModel, news);
                    var categoryDbSet = unitOfWork.Repository<Category>();
                    if ((news.Category == null) || (news.Category.Id != newsViewModel.CategoryId))
                    {
                        if (news.Category != null)
                        {
                            var category = await categoryDbSet.GetAsync(news.Category.Id);
                            category.News.Remove(news);
                            await categoryDbSet.EditAsync(category);
                        }
                        var currentCategory = await categoryDbSet.GetAsync(newsViewModel.CategoryId);
                        news.Category = currentCategory;
                    }
                    if ((news.UpdatedBy == null) || (news.UpdatedBy.Id != newsViewModel.UpdatedById))
                    {
                        var appUserReporitory = unitOfWork.Repository<AppUser>();
                        if (news.UpdatedBy != null)
                        {
                            var updatedBy = await appUserReporitory.GetAsync(news.UpdatedBy.Id);
                            updatedBy.UpdatedNews.Remove(news);
                            await appUserReporitory.EditAsync(updatedBy);
                        }
                        var newUpdatedBy = await appUserReporitory.GetAsync(newsViewModel.UpdatedById);
                        news.UpdatedBy = newUpdatedBy;
                        news.Updated = DateTime.Now;
                    }

                    await newsRepository.EditAsync(news);
                    await unitOfWork.SaveChangesAsync();

                    var tags = newsViewModel.StringTags.Split(",");
                    var newsTags = await GetTagsAsync(news.Id);

                    var addedTags = tags.Except(newsTags);
                    var removedTags = newsTags.Except(tags);

                    await AddToTagsAsync(news.Id, addedTags);
                    await RemoveFromTagsAsync(news.Id, removedTags);

                    await unitOfWork.SaveChangesAsync();

                    transaction.Commit();

                    newsViewModel = mapper.Map<NewsViewModel>(news);
                    return newsViewModel;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<List<SelectListItem>> GetСategoriesAsync()
        {
            var categories = await unitOfWork.Repository<Category>().GetAllAsync();
            var result = mapper.Map<List<SelectListItem>>(categories);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var news = await newsRepository.GetAsync(id);

                    await newsRepository.DeleteAsync(news);
                    var x = await unitOfWork.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        ///     Returns the tags for the news
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetTagsAsync(Guid newsId)
        {
            var news = await newsRepository.GetAsync(newsId);
            if (news == null)
            {
                throw new InvalidOperationException($"UserId not found. No user with this id found {newsId}.");
            }
            return await newsRepository.GetTagsAsync(news);
        }

        /// <summary>
        /// Method to add news to multiple tags
        /// </summary>
        /// <param name="newsId">news id</param>
        /// <param name="tags">list of tag names</param>
        /// <returns></returns>
        public async Task AddToTagsAsync(Guid newsId, IEnumerable<string> tags)
        {
            if (tags == null)
            {
                throw new ArgumentNullException(nameof(tags));
            }
            var news = await newsRepository.GetAsync(newsId);
            if (news == null)
            {
                throw new InvalidOperationException($"UserId not found. No user with this id found {newsId}.");
            }

            var newsTags = await newsRepository.GetTagsAsync(news);

            foreach (var tag in tags)
            {
                if (newsTags.Contains(tag))
                {
                    throw new ArgumentException("News already has tag. Error when a news is already has a tag");
                }
                await newsRepository.AddToTagAsync(news, tag);
            }
            await newsRepository.EditAsync(news);
        }


        /// <summary>
        /// Remove news from multiple tags
        /// </summary>
        /// <param name="newsId">news id</param>
        /// <param name="tags">list of tag names</param>
        /// <returns></returns>
        public async Task RemoveFromTagsAsync(Guid newsId, IEnumerable<string> tags)
        {
            //var userRoleStore = GetUserRoleStore();
            if (tags == null)
            {
                throw new ArgumentNullException(nameof(tags));
            }
            var news = await newsRepository.GetAsync(newsId);
            if (news == null)
            {
                throw new InvalidOperationException($"News not found. No news with this id found {newsId}.");
            }

            var newsTags = await newsRepository.GetTagsAsync(news);
            foreach (var tag in tags)
            {
                if (!newsTags.Contains(tag))
                {
                    throw new ArgumentException("News has't tag. Error when a news has't a tag");
                }
                await newsRepository.RemoveFromTagAsync(news, tag);
            }

            await newsRepository.EditAsync(news);
        }


        public async Task RateAsync(Guid? newsId, Guid? userId, int value)
        {
            if (newsId == null)
            {
                throw new ArgumentNullException(nameof(newsId));
            }

            var news = await newsRepository.GetAsync(newsId.GetValueOrDefault());

            if (news == null)
            {
                throw new InvalidOperationException($"News not found. No news with this id found {newsId}.");
            }

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var user = await unitOfWork.Repository<AppUser>().GetAsync(userId.GetValueOrDefault());

            if (user == null)
            {
                throw new InvalidOperationException($"User not found. No user with this id found {userId}.");
            }

            using (var transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    await newsRepository.RateAsync(news, user, value);
                    await unitOfWork.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
