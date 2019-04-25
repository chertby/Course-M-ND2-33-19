using System.Collections.Generic;

namespace Htp.ITnews.Data.Contracts.Entities
{
    public class Category : Entity
    {
        public string Title { get; set; }
        public IEnumerable<News> News { get; set; }
    }
}
