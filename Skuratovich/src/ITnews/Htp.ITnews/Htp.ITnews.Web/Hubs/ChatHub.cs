using System;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Web.Helpers;
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
        }

        public async Task SendComment(Guid newsId, string content)
        {
            if (newsId == null)
            {
                throw new ArgumentNullException(nameof(newsId));
            }

            var comment = new CommentViewModel
            {
                NewsId = newsId,
                AuthorId = Context.User.GetUserId(),
                Content = content
            };

            comment = await commentService.AddCommentAsync(comment);

            // Broadcast to current news
            await Clients.Group(newsId.ToString()).ReceiveComment(
                comment.Id,
                comment.AuthorUserName,
                comment.Content,
                comment.Created);
        }



        //public async Task SendComment(Guid newsId, string content)
        //{

        //    var comment = new CommentViewModel
        //    {
        //        NewsId = newsId,
        //        //AuthorId = Context.User.GetUserId(),
        //        AuthorId = new Guid(Context.UserIdentifier),

        //        Content = content
        //        //Created = DateTime.UtcNow
        //        //    SentAt = DateTimeOffset.UtcNow
        //    };

        //    //await chatRoomService.AddComment(comment);

        //    //// Broadcast to current news
        //    /// 


        //    await Clients.Group(newsId.ToString()).SendAsync(
        //        "ReceiveComment",
        //        comment.AuthorId,
        //        comment.Content
        //        );


        //    //await Clients.All.SendAsync(
        //    //"ReceiveMessage",
        //    //message.SenderName,
        //    //message.SentAt,
        //    //message.Text);

        //}

        //public async Task RequestToJoin(string groupName)
        //{

        //}
    }
}
