using System;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Domain.Contracts
{
    public interface ICommentService
    {
        IQueryable<CommentViewModel> GetAll(Guid newsId);
        Task<CommentViewModel> AddAsync(CommentViewModel commentViewModel);
    }
}
