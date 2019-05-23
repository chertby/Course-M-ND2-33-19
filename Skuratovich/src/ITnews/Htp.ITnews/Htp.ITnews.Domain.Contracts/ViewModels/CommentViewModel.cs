using System;
using System.ComponentModel.DataAnnotations;

namespace Htp.ITnews.Domain.Contracts.ViewModels
{
    public class CommentViewModel : IViewModel, IResource
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid NewsId { get; set; }
        public Guid AuthorId { get; set; }
        [Display(Name = "Author")]
        public string AuthorUserName { get; set; }
        public DateTime Created { get; set; }
        public Guid UpdatedById { get; set; }
        public string UpdatedByUserName { get; set; }
        public DateTime Updated { get; set; }
    }
}
