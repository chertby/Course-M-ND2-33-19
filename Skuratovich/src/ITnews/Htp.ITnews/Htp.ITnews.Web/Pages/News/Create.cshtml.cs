using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Web.Pages.News
{
    [Authorize(Roles = "Administrator,Writer")]
    public class CreateModel : PageModel
    {
        private readonly INewsService newsService;
        private readonly ITagService tagService;

        [BindProperty]
        public NewsViewModel NewsViewModel { get; set; }
        public List<SelectListItem> Сategories { get; private set; }

        public CreateModel(INewsService newsService, ITagService tagService)
        {
            this.newsService = newsService;
            this.tagService = tagService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NewsViewModel.AuthorId = User.GetUserId();

            await newsService.AddAsync(NewsViewModel);

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetTags(string term)
        {
            var tags = await tagService.GetTagsByTermAsync(term);
            return new JsonResult(tags);
        }

        private async Task PopulateLists()
        {
            Сategories = await newsService.GetСategoriesAsync();
        }
    }
}