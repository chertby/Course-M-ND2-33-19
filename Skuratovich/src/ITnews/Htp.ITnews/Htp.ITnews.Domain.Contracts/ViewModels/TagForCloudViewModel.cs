using System;
namespace Htp.ITnews.Domain.Contracts.ViewModels
{
    public class TagForCloudViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
    }
}
