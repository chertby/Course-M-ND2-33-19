using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Web.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Web.Pages.News
{
    [Authorize(Roles = "Administrator,Writer")]
    public class DeleteModel : PageModel
    {
        private readonly INewsService newsService;
        private readonly IAuthorizationService authorizationService;

        [BindProperty]
        public NewsViewModel NewsViewModel { get; set; }
    
        public DeleteModel(INewsService newsService, IAuthorizationService authorizationService)
        {
            this.newsService = newsService;
            this.authorizationService = authorizationService;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NewsViewModel = await newsService.GetAsync(id.GetValueOrDefault());

            var isAuthorized = await authorizationService.AuthorizeAsync(
                                                  User, NewsViewModel, new EditRequirement());
            if (!isAuthorized.Succeeded)
            {
                return new ForbidResult();
            }

            if (NewsViewModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NewsViewModel = await newsService.GetAsync(id.GetValueOrDefault());

            var isAuthorized = await authorizationService.AuthorizeAsync(
                                                  User, NewsViewModel, new EditRequirement());
            if (!isAuthorized.Succeeded)
            {
                return new ForbidResult();
            }

            if (NewsViewModel != null)
            {
                await newsService.DeleteAsync(NewsViewModel.Id);
            }

            return RedirectToPage("../Index");
        }
    }
}