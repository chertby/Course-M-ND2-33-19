using System;
using System.Collections.Generic;

namespace Htp.ITnews.Data.Contracts.Entities
{
    public class News : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public AppUser Author { get; set; }
        public DateTime Created { get; set; }
        public AppUser UpdatedBy { get; set; }
        public DateTime Updated { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<NewsTag> NewsTags { get; set; }
    }
}
