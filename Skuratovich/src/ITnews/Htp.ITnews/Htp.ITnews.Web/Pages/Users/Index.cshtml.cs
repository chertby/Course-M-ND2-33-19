using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using Microsoft.EntityFrameworkCore;

namespace Htp.ITnews.Web.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly INewsService newsService;
        private readonly IUserService userService;
        private readonly IAuthorizationService authorizationService;

        public IndexModel(INewsService newsService, 
            IUserService userService,
            IAuthorizationService authorizationService)
        {
            this.newsService = newsService;
            this.userService = userService;
            this.authorizationService = authorizationService;
        }

        public PaginatedList<NewsViewModel> News { get; set; }
        public UserViewModel UserViewModel { get; set; }

        //[BindProperty]
        //public InputModel Input { get; set; }

        //public class InputModel
        //{
        //    [Required]
        //    [DataType(DataType.Text)]
        //    [Display(Name = "First name")]
        //    public string FirstName { get; set; }

        //    [Required]
        //    [DataType(DataType.Text)]
        //    [Display(Name = "Last name")]
        //    public string LastName { get; set; }
        //}

        public async Task<IActionResult> OnGetAsync(Guid? id, int? pageIndex)
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

            var newsViewModelIQ = newsService.GetAll().Where(x => x.AuthorId == id.GetValueOrDefault());

            int pageSize = 5;
            News = await PaginatedList<NewsViewModel>.CreateAsync(
                newsViewModelIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            //Input = new InputModel
            //{
            //    FirstName = UserViewModel.FirstName,
            //    LastName = UserViewModel.LastName
            //};

            return Page();
        }

      
        public async Task<IActionResult> OnPostUpdateValue(Guid? pk, string name, string value)
        {
            var success = false;
            var msg = "";

            if (pk == null)
            {
                return NotFound();
            }

            var user = await userService.FindByIdAsync(pk.GetValueOrDefault());
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{pk.ToString()}'.");
            }

            var isAuthorized = await authorizationService.AuthorizeAsync(
                                                  User, user, new EditRequirement());

            if (!isAuthorized.Succeeded)
            {
                return new ForbidResult();
            }

            try
            {
                user.GetType().GetProperty(name).SetValue(user, value);
                await userService.UpdateAsync(user);
                success = true;
            }
            catch
            {
                msg = "server error";
            }
            var response = new { value = value, success = success, msg = msg };
            return new JsonResult(response);
        }
    }
}