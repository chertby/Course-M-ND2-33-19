using System;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Htp.ITnews.Web.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly ICommentService commentService;

        public ChatHub(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public async Task AddToGroup(Guid newsId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                newsId.ToString());

            await LoadHistory(newsId);
        }

        [Authorize]
        public async Task SendComment(Guid newsId, string content)
        {
            var comment = new CommentViewModel
            {
                NewsId = newsId,
                AuthorId = Context.User.GetUserId(),
                Content = content
            };

            comment = await commentService.AddAsync(comment);

            await Clients.Group(newsId.ToString()).ReceiveComment(comment);
        }

        public async Task LoadHistory(Guid newsId)
        {
            var comments = await commentService.GetAllAsync(newsId, Context.User.GetUserId());
            await Clients.Caller.ReceiveComments(comments);
        }

        [Authorize]
        public async Task VoteAsync(Guid? id, string action)
        {
            // TODO: Add authorization

            await commentService.Vote(id, Context.User.GetUserId(), action);

            await Clients.Caller.Vote(id.GetValueOrDefault(), action);
        }
    }
}
