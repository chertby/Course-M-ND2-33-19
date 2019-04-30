using System;
using System.Collections.Generic;

namespace Htp.ITnews.Data.Contracts.Entities
{
    public class Category : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<News> News { get; set; }
    }
}
