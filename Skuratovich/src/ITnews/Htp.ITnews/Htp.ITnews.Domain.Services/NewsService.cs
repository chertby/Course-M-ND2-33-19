using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IEnumerable<NewsViewModel>> GetAllAsync()
        {
            var news = await newsRepository.GetAllAsync();
            var result = mapper.Map<IEnumerable<NewsViewModel>>(news);
            return result;
        }

        public async Task<NewsViewModel> GetAsync(Guid id)
        {
            var news = await newsRepository.GetAsync(id);
            var result = mapper.Map<NewsViewModel>(news);
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
                    await newsRepository.AddAsync(news);
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

        public async Task<NewsViewModel> EditAsync(NewsViewModel newsViewModel)
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var news = await newsRepository.GetAsync(newsViewModel.Id);
                    mapper.Map(newsViewModel, news);
                    var categoryDbSet = unitOfWork.Repository<Category>();
                    if (news.Category.Id != newsViewModel.CategoryId)
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

                    await newsRepository.EditAsync(news);
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
    }
}
