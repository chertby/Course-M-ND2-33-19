using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Data.EntityFramework.Extensions;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Domain.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<CommentViewModel> AddAsync(CommentViewModel commentViewModel)
        {
            var comment = mapper.Map<Comment>(commentViewModel);

            using (var transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    comment.News = await unitOfWork.Repository<News>().GetAsync(commentViewModel.NewsId);
                    comment.Author = await unitOfWork.Repository<AppUser>().GetAsync(commentViewModel.AuthorId);
                    comment.Created = DateTime.Now;
                    await unitOfWork.Repository<Comment>().AddAsync(comment);

                    await unitOfWork.SaveChangesAsync();

                    transaction.Commit();

                    commentViewModel = mapper.Map<CommentViewModel>(comment);

                    return commentViewModel;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<IQueryable<CommentViewModel>> GetAllAsync(Guid newsId, Guid userId)
        {
            if (newsId == null)
            {
                throw new ArgumentNullException(nameof(newsId));
            }

            var news = await unitOfWork.Repository<News>().GetAsync(newsId);

            if (news == null)
            {
                throw new InvalidOperationException($"News not found. No news with this id found {newsId}.");
            }

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var result = commentRepository
                .FindByCondition(c => c.News.Id == newsId, x => x
                    .Include(c => c.Likes))
                .OrderBy(c => c.Created)
                .Select(c => new CommentViewModel() { 
                    Id = c.Id,
                    NewsId = c.News.Id,
                    Content = c.Description,
                    AuthorId = c.Author.Id,
                    AuthorUserName = c.Author.UserName,
                    Created = c.Created,
                    UpdatedById = c.UpdatedBy == null ? new Guid() : c.UpdatedBy.Id,
                    UpdatedByUserName = c.UpdatedBy == null ? "" : c.UpdatedBy.UserName,
                    Updated = c.Updated,
                    IsLiked = c.Likes.Any(l => l.AppUserId == userId),
                    Likes = c.Likes.Count });

            //var result = comments.ProjectTo<CommentViewModel>(mapper.ConfigurationProvider);
            return result;
        }

        public async Task<CommentViewModel> GetAsync(Guid id)
        {
            var comment = await commentRepository.GetAsync(id);
            var result = mapper.Map<CommentViewModel>(comment);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var comment = await commentRepository.GetAsync(id);

                    await commentRepository.DeleteAsync(comment);
                    var x = await unitOfWork.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task VoteAsync(Guid? commentId, Guid? userId, string action)
        {
            if (commentId == null)
            {
                throw new ArgumentNullException(nameof(commentId));
            }

            var comment = await commentRepository.GetAsync(commentId.GetValueOrDefault());

            if (comment == null)
            {
                throw new InvalidOperationException($"Comment not found. No comment with this id found {commentId}.");
            }

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var user = await unitOfWork.Repository<AppUser>().GetAsync(userId.GetValueOrDefault());

            if (user == null)
            {
                throw new InvalidOperationException($"User not found. No user with this id found {userId}.");
            }

            if (string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentException("Value cannot be null or empty. Null or empty error message.", nameof(action));
            }

            using (var transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    if (action == "like")
                    {
                        await commentRepository.AddLikeAsync(comment, user);
                        await unitOfWork.SaveChangesAsync();
                    }
                    else if (action == "dislike")
                    {
                        await commentRepository.RemoveLikeAsync(comment, user);
                        await unitOfWork.SaveChangesAsync();
                    }
                    else
                    {
                        throw new ArgumentException("Invalid value", nameof(action));
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
