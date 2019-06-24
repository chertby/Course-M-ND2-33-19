using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Htp.ITnews.Domain.Contracts.ViewModels
{
    public class NewsViewModel : IViewModel, IResource
    {
        public Guid Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Content")]
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        [Display(Name = "Author")]
        public string AuthorUserName { get; set; }
        public DateTime Created { get; set; }
        public Guid UpdatedById { get; set; }
        public string UpdatedByUserName { get; set; }
        public DateTime Updated { get; set; }
        public Guid CategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryTitle { get; set; }
        public string StringTags { get; set; }
        public IList<TagViewModel> Tags { get; set; }
        public decimal Rating { get; set; }
        public int CommentCount { get; set; }
    }
}
