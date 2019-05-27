using System;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Web.Hubs
{
    public interface IChatClient
    {
        //Task ReceiveComment(Guid id, string authorUserName, string content, DateTime created, bool isAuthor);
        Task ReceiveComment(CommentViewModel comment);
        Task ReceiveComments(IQueryable<CommentViewModel> comments);
    }
}
