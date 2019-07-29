using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Htp.ITnews.Domain.Contracts.ViewModels
{
    public class UserViewModel : IdentityUser<Guid>, IViewModel
    {
        [Display(Name = "Is active")]
        public bool IsActive { get; set; }
        [PersonalData]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [PersonalData]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "User name")]
        public new string UserName { get ; set; }

        public IEnumerable<NewsViewModel> News { get; set; }
    }
}
