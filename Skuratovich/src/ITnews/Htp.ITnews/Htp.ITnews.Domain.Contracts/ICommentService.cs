using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Domain.Contracts
{
    public interface ICommentService
    {
        Task<CommentViewModel> AddCommentAsync(CommentViewModel commentViewModel);
    }
}
