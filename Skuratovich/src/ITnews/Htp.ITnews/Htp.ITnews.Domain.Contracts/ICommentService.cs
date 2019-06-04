using System;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Domain.Contracts
{
    public interface ICommentService
    {
        Task<IQueryable<CommentViewModel>> GetAllAsync(Guid newsId, Guid userId);
        Task<CommentViewModel> AddAsync(CommentViewModel commentViewModel);
        Task<CommentViewModel> GetAsync(Guid id);
        Task VoteAsync(Guid? commentId, Guid? userId, string action);
    }
}
