using System;
using System.Collections.Generic;

namespace Htp.ITnews.Data.Contracts.Entities
{
    public class News : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public AppUser Author { get; set; }
        public DateTime Created { get; set; }
        public AppUser UpdatedBy { get; set; }
        public DateTime Updated { get; set; }
        public Category Category { get; set; }
        public decimal Rating { get; set; }
        public int RatingSum { get; set; }
        public int RatingCount { get; set; }
        public int CommentCount { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; }
    }
}
