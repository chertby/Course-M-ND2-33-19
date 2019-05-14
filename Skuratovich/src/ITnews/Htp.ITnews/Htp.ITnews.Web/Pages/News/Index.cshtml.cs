using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Htp.ITnews.Web.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly INewsService newsService;
        public string CurrentFilter { get; set; }

        public PaginatedList<NewsViewModel> News { get; set; }

        public IndexModel(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            var newsViewModelIQ = newsService.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                newsViewModelIQ = newsViewModelIQ.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }

            int pageSize = 3;
            News = await PaginatedList<NewsViewModel>.CreateAsync(
                newsViewModelIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
