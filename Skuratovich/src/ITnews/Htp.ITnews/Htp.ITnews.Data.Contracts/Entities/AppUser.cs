using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Htp.ITnews.Data.Contracts.Entities
{
    public class AppUser : IdentityUser<Guid>, IEntity
    {
        public bool IsActive { get; set; }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        //public ICollection<News> News { get; set; }
        //public ICollection<News> UpdatedNews { get; set; }
        //public ICollection<Comment> Comments { get; set; }
        //public ICollection<Comment> UpdatedComments { get; set; }
        //public ICollection<Rating> Ratings { get; set; }
        //public ICollection<Like> Likes { get; set; }
    }
}
