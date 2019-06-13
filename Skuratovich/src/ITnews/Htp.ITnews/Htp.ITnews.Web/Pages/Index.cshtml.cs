using System;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
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
        public bool CurrentOrder { get; set; }

        public PaginatedList<NewsViewModel> News { get; set; }

        public IndexModel(INewsService newsService, ITagService tagService)
        {
            this.newsService = newsService;
            this.tagService = tagService;
        }

        public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex, string tagString, string orderBy)
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

            IQueryable<NewsViewModel> newsViewModelIQ;

            if ((!string.IsNullOrEmpty(tagString)) && Guid.TryParse(tagString, out Guid tagId))
            {
                newsViewModelIQ = newsService.GetAllByTag(tagId);
            }
            else
            {
                newsViewModelIQ = newsService.GetAll();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                newsViewModelIQ = newsViewModelIQ.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }

            if (string.IsNullOrEmpty(orderBy))
            {
                newsViewModelIQ = newsViewModelIQ.OrderByDescending(n => n.Updated);
                CurrentOrder = true;
            }
            else
            {
                if (orderBy == "updated")
                {
                    newsViewModelIQ = newsViewModelIQ.OrderByDescending(n => n.Updated);
                    CurrentOrder = true;
                }
                else
                {
                    newsViewModelIQ = newsViewModelIQ.OrderByDescending(n => n.Rating);
                    CurrentOrder = false;
                }
            }

            int pageSize = 5;
            News = await PaginatedList<NewsViewModel>.CreateAsync(
                newsViewModelIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        public IActionResult OnGetTagsForCloud()
        {
            var tags = tagService.GetTagsForCloud();
            return new JsonResult(tags);
        }

        public IActionResult OnPostSetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

    }
}
