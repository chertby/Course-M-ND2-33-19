using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Web.Pages.News
{
    [Authorize(Policy = "EditPolicy")]
    public class EditModel : PageModel
    {
        private readonly INewsService newsService;

        [BindProperty]
        public NewsViewModel NewsViewModel { get; set; }
        public List<SelectListItem> Сategories { get; private set; }

        public EditModel(INewsService newsService)
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

            await PopulateLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: add UpdatedBy
            //BookViewModel.CreatedByUserID = User.GetUserId();

            await newsService.EditAsync(NewsViewModel);

            return RedirectToPage("./Index");
        }

        private async Task PopulateLists()
        {
            Сategories = await newsService.GetСategoriesAsync();
        }
    }
}