using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Web.Pages.News
{
    public class DetailsModel : PageModel
    {
        private readonly INewsService newsService;

        [BindProperty]
        public NewsViewModel NewsViewModel { get; set; }

        public DetailsModel(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NewsViewModel = await newsService.GetAsync(id.GetValueOrDefault());

            if (NewsViewModel == null)
            {
                return NotFound();
            }

            var newsTags = await newsService.GetTagsAsync(id.GetValueOrDefault());

            NewsViewModel.StringTags = string.Join(",", newsTags);

            return Page();
        }
    }
}