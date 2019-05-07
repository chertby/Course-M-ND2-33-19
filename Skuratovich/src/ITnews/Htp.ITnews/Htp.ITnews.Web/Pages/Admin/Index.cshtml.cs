using System.Collections.Generic;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Htp.ITnews.Web.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IUserService userService;

        public IList<UserViewModel> Users  { get; set; }

        public IndexModel(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task OnGetAsync()
        {
            var result = await userService.GetAllUsersAsync();
            Users = (List<UserViewModel>)result;
        }
    }
}
