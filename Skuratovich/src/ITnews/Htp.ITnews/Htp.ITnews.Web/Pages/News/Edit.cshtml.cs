using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Web.Authorization.Requirements;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Web.Pages.News
{
    [Authorize(Roles = "Administrator,Writer")]
    public class EditModel : PageModel
    {
        private readonly INewsService newsService;
        private readonly ITagService tagService;
        private readonly IAuthorizationService authorizationService;

        [BindProperty]
        public NewsViewModel NewsViewModel { get; set; }
        public List<SelectListItem> Сategories { get; private set; }

        public EditModel(INewsService newsService,
            ITagService tagService,
            IAuthorizationService authorizationService)
        {
            this.newsService = newsService;
            this.tagService = tagService;
            this.authorizationService = authorizationService;
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

            var isAuthorized = await authorizationService.AuthorizeAsync(
                                                  User, NewsViewModel, new EditRequirement());
            if (!isAuthorized.Succeeded)
            {
                return new ForbidResult();
            }

            NewsViewModel.StringTags = string.Join(",", NewsViewModel.Tags);

            await PopulateLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorized = await authorizationService.AuthorizeAsync(
                                                  User, NewsViewModel, new EditRequirement());
            if (!isAuthorized.Succeeded)
            {
                return new ForbidResult();
            }

            NewsViewModel.UpdatedById = User.GetUserId();

            await newsService.EditAsync(NewsViewModel);

            return RedirectToPage("../Index");
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

        public async Task<IActionResult> OnGetTest()
        {
            var test = await tagService.Test();
            return new JsonResult(test);
        }
    }
}
