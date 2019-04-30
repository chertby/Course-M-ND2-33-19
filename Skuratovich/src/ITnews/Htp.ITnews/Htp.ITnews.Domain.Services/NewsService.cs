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
                    news.Category = await unitOfWork.Repository<Category>().GetAsync(news.Category.Id);
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
                    //        if (book.UpdatedBy != null)
                    //        {
                    //            var updatedBefore = unitOfWork.Repository<AppUser>().Get(book.UpdatedBy.Id);
                    //            updatedBefore.UpdatedBooks.Remove(book);
                    //            unitOfWork.Repository<AppUser>().Update(updatedBefore);
                    //            var x = await unitOfWork.SaveChangesAsync();
                    //        }

                    //        var UpdatedBy = unitOfWork.Repository<AppUser>().Get(bookViewModel.UpdatedByUserID);
                    //        book.UpdatedBy = UpdatedBy;

                    //        unitOfWork.BookRepository.Update(book);
                    //        var y = await unitOfWork.SaveChangesAsync();
                    //        transaction.Commit();
                    //        return true;

                    news.Category = await unitOfWork.Repository<Category>().GetAsync(news.Category.Id);

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
                    //return false;
                }
            }
        }

        public async Task<List<SelectListItem>> GetСategoriesAsync()
        {
            var categories = new List<SelectListItem>();

            foreach (var category in await unitOfWork.Repository<Category>().GetAllAsync())
            {
                categories.Add(new SelectListItem() { Value = category.Id.ToString(), Text = category.Title });
            }
            return categories;
        }
    }
}
