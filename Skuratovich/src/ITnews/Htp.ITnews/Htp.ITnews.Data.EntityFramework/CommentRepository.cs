using System;
using System.Threading.Tasks;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Htp.ITnews.Data.EntityFramework
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddLikeAsync(Comment comment, AppUser user)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var likes = dbContext.Like;

            var like = await likes.FirstOrDefaultAsync(l => l.CommentId.Equals(comment.Id) && l.AppUserId.Equals(user.Id));
            if (like == null)
            {
                like = new Like { CommentId = comment.Id, AppUserId = user.Id };
                await likes.AddAsync(like);
            }
        }

        public async Task RemoveLikeAsync(Comment comment, AppUser user)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var likes = dbContext.Like;
            var like = await likes.FirstOrDefaultAsync(l => l.CommentId.Equals(comment.Id) && l.AppUserId.Equals(user.Id));
            if (like != null)
            {
                likes.Remove(like);
            }
        }
    }
}
