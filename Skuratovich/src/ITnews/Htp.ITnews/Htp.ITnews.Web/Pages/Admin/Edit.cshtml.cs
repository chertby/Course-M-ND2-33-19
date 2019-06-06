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

namespace Htp.ITnews.Web.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        [BindProperty]
        public UserViewModel UserViewModel { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; private set; }
        public IEnumerable<string> UserRoles { get; private set; }

        public EditModel(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
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
                return NotFound();
            }

            await PopulateLists(UserViewModel);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IEnumerable<string> roles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userService.FindByIdAsync(UserViewModel.Id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserViewModel.Id}'.");
            }

            if (UserViewModel.IsActive != user.IsActive)
            {
                user.IsActive = UserViewModel.IsActive;

                await userService.UpdateAsync(user);

                if (!user.IsActive)
                {
                    await userService.UpdateSecurityStampAsync(user);
                }
            }



            var userRoles = await userService.GetRolesAsync(UserViewModel);
            var addedRoles = roles.Except(userRoles);
            var removedRoles = userRoles.Except(roles);

            await userService.AddToRolesAsync(UserViewModel, addedRoles);
            await userService.RemoveFromRolesAsync(UserViewModel, removedRoles);

            return RedirectToPage("./Index");
        }

        private async Task PopulateLists(UserViewModel userViewModel)
        {
            Roles = await roleService.GetRolesAsync();
            UserRoles = await userService.GetRolesAsync(userViewModel);
        }
    }
}