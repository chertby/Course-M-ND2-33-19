using System;
using System.ComponentModel.DataAnnotations;

namespace Htp.ITnews.Domain.Contracts.ViewModels
{
    public class NewsViewModel : IViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}
