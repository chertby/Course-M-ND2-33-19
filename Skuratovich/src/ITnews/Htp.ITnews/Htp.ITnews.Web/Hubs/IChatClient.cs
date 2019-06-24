using System;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Web.Hubs
{
    public interface IChatClient
    {
        Task ReceiveComment(CommentViewModel comment, bool isInRole);
        Task ReceiveComments(IQueryable<CommentViewModel> comments, bool isInRole);
        Task Vote(Guid id, string action);
        Task ClearComment();
        Task UpdateLike(Guid id, int count);
    }
}
