using System;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Htp.ITnews.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly INewsService newsService;
        private readonly ITagService tagService;

        public string CurrentFilter { get; set; }

        public PaginatedList<NewsViewModel> News { get; set; }

        public IndexModel(INewsService newsService, ITagService tagService)
        {
            this.newsService = newsService;
            this.tagService = tagService;
        }

        public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex, string tagString)
        {
            if ((searchString != null) || (tagString != null))
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            //var newsViewModelIQ = newsService.GetAll();
            //var newsViewModelIQ;

            var newsViewModelIQ = ((!string.IsNullOrEmpty(tagString)) && Guid.TryParse(tagString, out Guid tagId)) ? newsService.GetAllByTag(tagId) : newsService.GetAll();

            //if ((!string.IsNullOrEmpty(tagString)) && Guid.TryParse(tagString, out Guid tagId))
            //{
            //    var newsViewModelIQ = newsService.GetAllByTag(tagId);
            //}
            //else
            //{
            //    var newsViewModelIQ = newsService.GetAll();
            //}


            if (!string.IsNullOrEmpty(searchString))
            {
                newsViewModelIQ = newsViewModelIQ.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }


            int pageSize = 10;
            News = await PaginatedList<NewsViewModel>.CreateAsync(
                newsViewModelIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        public async Task<IActionResult> OnGetTagsForCloudAsync()
        {
            var tags = await tagService.GetTagsForCloudAsync();
            return new JsonResult(tags);
        }

    }
}
