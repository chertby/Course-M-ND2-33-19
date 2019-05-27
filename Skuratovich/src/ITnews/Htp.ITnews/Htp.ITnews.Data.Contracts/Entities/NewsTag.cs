using System;
namespace Htp.ITnews.Data.Contracts.Entities
{
    public class NewsTag : IEntity
    {
        public Guid Id { get; set; }
        public Guid NewsId { get; set; }
        public News News { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
