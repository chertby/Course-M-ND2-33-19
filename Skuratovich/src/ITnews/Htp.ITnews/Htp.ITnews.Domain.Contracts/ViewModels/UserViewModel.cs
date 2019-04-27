using System;
using Microsoft.AspNetCore.Identity;

namespace Htp.ITnews.Domain.Contracts.ViewModels
{
    public class UserViewModel : IdentityUser<Guid>, IViewModel
    {
        public bool IsActive { get; set; }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
    }
}
