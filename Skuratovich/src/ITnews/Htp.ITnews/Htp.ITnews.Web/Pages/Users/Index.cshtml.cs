using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Web.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly INewsService newsService;
        private readonly IUserService userService;

        [BindProperty]
        public NewsViewModel NewsViewModel { get; set; }

        public UserViewModel UserViewModel { get; set; }


        public DetailsModel(INewsService newsService, IUserService userService)
        {
            this.newsService = newsService;
            this.userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserViewModel = await userService.FindByIdAsync(id.GetValueOrDefault());
            if (UserViewModel == null)
            {
                return NotFound($"Unable to load user with ID '{id.ToString()}'.");
            }

            //NewsViewModel = await newsService.GetAsync(id.GetValueOrDefault());

            //if (NewsViewModel == null)
            //{
            //    return NotFound();
            //}

            return Page();
        }
    }
}