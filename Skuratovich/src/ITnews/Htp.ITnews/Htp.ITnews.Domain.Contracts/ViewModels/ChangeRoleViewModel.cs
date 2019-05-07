using System;
using System.Collections.Generic;

namespace Htp.ITnews.Domain.Contracts.ViewModels
{
    public class ChangeRoleViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        //public List<IdentityRole> AllRoles { get; set; }
        //public IList<string> UserRoles { get; set; }

        public ChangeRoleViewModel()
        {
        }
    }
}
