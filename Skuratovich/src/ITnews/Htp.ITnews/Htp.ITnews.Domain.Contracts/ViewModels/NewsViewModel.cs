using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Domain.Contracts.ViewModels
{
    public class NewsViewModel : IViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }
    }
}
