using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Domain.Contracts
{
    public interface INewsService
    {
        IQueryable<NewsViewModel> GetAll();
        IQueryable<NewsViewModel> GetAllByTag(Guid tagId);
        Task<NewsViewModel> GetAsync(Guid id);
        NewsViewModel Get(Guid id, Guid userId);
        Task<NewsViewModel> AddAsync(NewsViewModel newsViewModel);
        Task<NewsViewModel> EditAsync(NewsViewModel newsViewModel);
        Task DeleteAsync(Guid id);

        Task<List<SelectListItem>> GetСategoriesAsync();

        Task<IList<string>> GetTagsAsync(Guid newsId);
        Task AddToTagsAsync(Guid newsId, IEnumerable<string> tags);

        Task RateAsync(Guid? newsId, Guid? userId, int value);
    }
}
