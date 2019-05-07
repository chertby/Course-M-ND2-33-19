using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Domain.Contracts.ViewModels
{
    public class NewsViewModel : IViewModel, IResource
    {
        public Guid Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime Created { get; set; }
        //public AppUser UpdatedBy { get; set; }
        //public DateTime Updated { get; set; }
        public Guid CategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryTitle { get; set; }
    }
}
