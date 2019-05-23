using System;
using System.Threading.Tasks;
using AutoMapper;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Domain.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<CommentViewModel> AddCommentAsync(CommentViewModel commentViewModel)
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
    }
}
