using System.Collections.Generic;

namespace Htp.ITnews.Data.Contracts.Entities
{
    public class Tag : Entity
    {
        public string Title { get; set; }
        public IEnumerable<NewsTag> NewsTags { get; set; }
    }
}
