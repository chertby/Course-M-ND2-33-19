using System;
namespace Htp.ITnews.Domain.Contracts.ViewModels
{
    public class RatingViewModel
    {
        public Guid NewsId { get; set; }
        public Guid UserId { get; set; }
        public int Value { get; set; }
    }
}
