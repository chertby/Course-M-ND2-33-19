using System;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Web.Authorization.Requirements;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Htp.ITnews.Web.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly ICommentService commentService;
        private readonly IAuthorizationService authorizationService;

        public ChatHub(ICommentService commentService, IAuthorizationService authorizationService)
        {
            this.commentService = commentService;
            this.authorizationService = authorizationService;
        }

        public async Task AddToGroup(Guid newsId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                newsId.ToString());

            await LoadHistory(newsId);
        }

        [Authorize(Policy = "RequireRole")]
        public async Task SendComment(Guid newsId, string content)
        {
            var comment = new CommentViewModel
            {
                NewsId = newsId,
                AuthorId = Context.User.GetUserId(),
                Content = content
            };

            comment = await commentService.AddAsync(comment);

            var isAuthorized = await authorizationService.AuthorizeAsync(Context.User, "RequireRole");
            var isInRoles = isAuthorized.Succeeded;

            await Clients.Caller.ClearComment();
            await Clients.Group(newsId.ToString()).ReceiveComment(comment, isInRoles);

        }

        public async Task LoadHistory(Guid newsId)
        {
            var isAuthorized = await authorizationService.AuthorizeAsync(Context.User, "RequireRole");
            var isInRoles = isAuthorized.Succeeded;

            var comments = await commentService.GetAllAsync(newsId, Context.User.GetUserId());
            await Clients.Caller.ReceiveComments(comments, isInRoles);
        }

        [Authorize(Policy = "RequireRole")]
        public async Task VoteAsync(Guid? id, string action)
        {
            var comment = await commentService.VoteAsync(id, Context.User.GetUserId(), action);

            if (comment != null)
            {
                await Clients.Group(comment.NewsId.ToString()).UpdateLike(comment.Id, comment.LikesCount);
            }

            await Clients.Caller.Vote(id.GetValueOrDefault(), action);
        }
    }
}
