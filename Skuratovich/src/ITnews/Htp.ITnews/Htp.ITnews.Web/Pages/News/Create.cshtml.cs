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

        [BindProperty]
        public NewsViewModel NewsViewModel { get; set; }
        public List<SelectListItem> Сategories { get; private set; }

        public CreateModel(INewsService newsService)
        {
            this.newsService = newsService;
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

        private async Task PopulateLists()
        {
            Сategories = await newsService.GetСategoriesAsync();
        }
    }
}