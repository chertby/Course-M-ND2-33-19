using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Htp.ITnews.Data.Contracts.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<News> UpdatedNews { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
