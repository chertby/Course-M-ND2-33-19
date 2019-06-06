using System;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Web.Hubs
{
    public interface IChatClient
    {
        Task ReceiveComment(CommentViewModel comment);
        Task ReceiveComments(IQueryable<CommentViewModel> comments);
        Task Vote(Guid id, string action);
        Task ClearComment();
        Task UpdateLike(Guid id, int count);
    }
}
