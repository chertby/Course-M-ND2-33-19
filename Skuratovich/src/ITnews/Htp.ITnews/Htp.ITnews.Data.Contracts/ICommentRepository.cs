using System.Threading.Tasks;
using Htp.ITnews.Data.Contracts.Entities;

namespace Htp.ITnews.Data.Contracts
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task AddLikeAsync(Comment comment, AppUser user);
        Task RemoveLikeAsync(Comment comment, AppUser user);
    }
}
