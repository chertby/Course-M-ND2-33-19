using System;
namespace Htp.ITnews.Data.Contracts.Entities
{
    public class Comment : Entity
    {
        public string Description { get; set; }
        public News News { get; set; }
        public AppUser Author { get; set; }
    }
}
