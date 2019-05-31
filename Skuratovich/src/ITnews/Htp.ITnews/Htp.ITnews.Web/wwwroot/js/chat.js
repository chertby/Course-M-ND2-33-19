// Initialize the SignalR client
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

//Disable send button until connection is established
if (!!document.getElementById("sendButton"))
{
    document.getElementById("sendButton").disabled = true;
}

connection.on('ReceiveComment', addComment);
connection.on('ReceiveComments', addComments);

connection.start().then(function(){
    var newsId = document.getElementById("NewsViewModel_Id").value;
    connection.invoke("AddToGroup", newsId).catch(function (err) {
        return console.error(err.toString());
    });
    if (!!document.getElementById("sendButton"))
    {
        document.getElementById("sendButton").disabled = false;
    }
}).catch(function (err) {
    return console.error(err.toString());
});

if (!!document.getElementById("sendButton"))
{
document.getElementById("sendButton").addEventListener("click", function (event) {
    var newsId = document.getElementById("NewsViewModel_Id").value;
    var content = document.getElementById("messageInput").value;
    connection.invoke("SendComment", newsId, content).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
}

function addComments(comments) {
    if (!comments) return;
    comments.forEach(function (c) {
        addComment(c);
    });
}

function addComment(c) {
    var authorSpan = document.createElement('span');
    authorSpan.className = 'user-info_nickname';
    authorSpan.textContent = c.authorUserName;

    var authorA = document.createElement('a');
    authorA.setAttribute('href', '../Users?id='+c.authorId);
    authorA.className = 'user-info';
    authorA.appendChild(authorSpan);

    var commentTime = document.createElement('time');
    commentTime.className = 'comment__date-time';
    commentTime.textContent = moment(c.created).format('LLL');

    
    var hrefUse = document.createElement('use');
    //hrefUse.setAttribute('xlink:href', 'https://localhost:5001/img/common-svg-sprite.svg#anchor');
    //hrefUse.setAttribute('xlink:href', '#anchor');

    var hrefPath = document.createElement('path');
    hrefPath.setAttribute('d', 'M4 16v-8h-4v-4h4v-4h4v4h8v-4h4v4h4v4h-4v8h4v4h-4v4h-4v-4h-8v4h-4v-4h-4v-4h4zm4 0h8v-8h-8v8z');

    var hrefSvg = document.createElement('svg');
    hrefSvg.setAttribute('width', '12');
    hrefSvg.setAttribute('height', '12');
    //hrefSvg.appendChild(hrefUse);
    hrefSvg.appendChild(hrefPath);

    var hrefA = document.createElement('a');
    hrefA.className = 'icon_comment-anchor';
    hrefA.setAttribute('href', '#'+c.id);
    hrefA.setAttribute('Title', 'Reference on comment');
    //hrefA.appendChild(hrefSvg);
    hrefA.textContent = 'ref';

    var hrefLi = document.createElement('li');
    hrefLi.className = 'inline-list inline-list_comment-nav';
    hrefLi.appendChild(hrefA);

    var editA = document.createElement('a');
    editA.className = 'icon_comment-anchor';
    editA.setAttribute('href', '#'+c.id);
    editA.setAttribute('Title', 'Edit comment');
    editA.textContent = 'edit';

    var editLi = document.createElement('li');
    editLi.className = 'inline-list inline-list_comment-nav';
    editLi.appendChild(editA);

    var deleteA = document.createElement('a');
    deleteA.className = 'icon_comment-anchor';
    deleteA.setAttribute('href', '#'+c.id);
    deleteA.setAttribute('Title', 'Delete comment');
    deleteA.textContent = 'delete';

    var deleteLi = document.createElement('li');
    deleteLi.className = 'inline-list inline-list_comment-nav';
    deleteLi.appendChild(deleteA);

    var actionUl = document.createElement('ul');
    actionUl.className = 'inline-list inline-list_comment-nav';
    actionUl.appendChild(hrefLi);
    actionUl.appendChild(editLi);
    actionUl.appendChild(deleteLi);

    var likeButton = document.createElement('button');
    likeButton.className = 'btn comment__like';
    likeButton.textContent = 'Like';
    likeButton.setAttribute('data-action', 'like');
    likeButton.setAttribute('type', 'button');
    likeButton.disabled = c.isLiked;
    likeButton.addEventListener("click", comment_vote);

    var dislikeButton = document.createElement('button');
    dislikeButton.className = 'btn comment__dislike';
    dislikeButton.textContent = 'Dislike';
    dislikeButton.setAttribute('data-action', 'dislike');
    dislikeButton.setAttribute('type', 'button');
    dislikeButton.disabled = !c.isLiked;
    dislikeButton.addEventListener("click", comment_vote);

    var commenLikeDiv = document.createElement('div');
    commenLikeDiv.className = 'comment__like js-comment-vote';
    commenLikeDiv.setAttribute('data-id', c.id);
    commenLikeDiv.appendChild(likeButton);
    commenLikeDiv.appendChild(dislikeButton);

    var commentHeaderDiv = document.createElement('div');
    commentHeaderDiv.className = 'comment__header';
    commentHeaderDiv.appendChild(authorA);
    commentHeaderDiv.appendChild(commentTime);
    commentHeaderDiv.appendChild(actionUl);
    commentHeaderDiv.appendChild(commenLikeDiv);

    var commentMessageDiv = document.createElement('div');
    commentMessageDiv.className = 'comment__message';
    commentMessageDiv.textContent = c.content.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var commenFooterDiv = document.createElement('div');
    commenFooterDiv.className = 'comment__footer';

    var commentDiv = document.createElement('div');
    commentDiv.className = 'comment';
    commentDiv.setAttribute('id', c.id);
    commentDiv.appendChild(commentHeaderDiv);
    commentDiv.appendChild(commentMessageDiv);
    commentDiv.appendChild(commenFooterDiv);
   
    var newComment = document.createElement('li');
    newComment.setAttribute('rel', c.id);
    newComment.appendChild(commentDiv);

    $('#commentsList').prepend(newComment);
}

function onclickLike(event) {
    alert('test');
}

function comment_vote(e) {
    var target = $(e.target),
        o = target.closest(".js-comment-vote"),
        i = {
            id: o.attr("data-id"),
            action: target.attr("data-action")
            };

    var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();

    connection.invoke("VoteAsync", i.id, i.action).catch(function (err) {
        return console.error(err.toString());
    });

    //$.ajaxSetup({
    //    headers:{
    //        'RequestVerificationToken': antiForgeryToken
    //    }
    //});

    //var commentsAPI = "?handler=Vote";
    //$.post(commentsAPI, i)
    //    .done(function (data) {
    //        alert('Handler: ' + i.action);
    //    })
    //    .fail(function () {
    //        console.log("error");
    //    });
    
}
