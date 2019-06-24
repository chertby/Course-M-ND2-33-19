using System;
namespace Htp.ITnews.Data.Contracts.Entities
{
    public class Rating
    {
        public Guid NewsId { get; set; }
        public News News { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int Value { get; set; }
    }
}
