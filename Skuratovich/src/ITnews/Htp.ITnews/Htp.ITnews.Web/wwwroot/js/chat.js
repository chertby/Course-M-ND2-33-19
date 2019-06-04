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
connection.on('ClearComment', clearComment);
connection.on('Vote', vote);

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
    var faceImg = document.createElement('img');
    faceImg.className = 'img img-rounded img-fluid';
    faceImg.setAttribute('src', '../img/def_face.jpg');

    var faceP = document.createElement('p');
    faceP.className = 'text-secondary text-center';
    faceP.textContent = moment(c.created).format('LLL');;

    var faceDiv = document.createElement('div');
    faceDiv.className = 'col-md-2';
    faceDiv.appendChild(faceImg);
    faceDiv.appendChild(faceP);
    
    var authorStrong = document.createElement('strong');
    authorStrong.textContent = c.authorUserName;

    var authorA = document.createElement('a');
    authorA.setAttribute('href', '../Users?id='+c.authorId);
    authorA.className = 'float-left';
    authorA.appendChild(authorStrong);
    
    var textP = document.createElement('p');
    textP.appendChild(authorA);

    var clearfixDiv = document.createElement('div');
    clearfixDiv.className = 'clearfix';
    
    var commentP = document.createElement('p');
    commentP.textContent = c.content.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var likeI = document.createElement('i');
    likeI.className = 'fa fa-thumbs-up';

    var likeA = document.createElement('button');
    likeA.className = 'float-right btn text-white btn-info js-comment-like js-comment-btn';
    likeA.appendChild(likeI);
    likeA.setAttribute('data-action', 'like');
    likeA.setAttribute('type', 'button');
    likeA.disabled = c.isLiked;
    likeA.addEventListener("click", comment_vote);
    //likeA.append(" Like");

    var dislikeI = document.createElement('i');
    dislikeI.className = 'fa fa-thumbs-down';

    var dislikeA = document.createElement('button');
    dislikeA.className = 'float-right btn text-white btn-dark js-comment-dislike js-comment-btn';
    dislikeA.appendChild(dislikeI);
    dislikeA.setAttribute('data-action', 'dislike');
    dislikeA.setAttribute('type', 'button');
    dislikeA.disabled = !c.isLiked;
    dislikeA.addEventListener("click", comment_vote);
    //dislikeA.append(" Like");

    var buttonsP = document.createElement('p');
    buttonsP.appendChild(likeA);
    buttonsP.appendChild(dislikeA);

    var textDiv = document.createElement('div');
    textDiv.className = 'col-md-10 js-comment-vote';
    textDiv.setAttribute('data-id', c.id);
    textDiv.setAttribute('data-news-target', c.newsId);
    textDiv.appendChild(textP);
    textDiv.appendChild(clearfixDiv);
    textDiv.appendChild(commentP);
    textDiv.appendChild(buttonsP);

    var rowDiv = document.createElement('div');
    rowDiv.className = 'row';
    rowDiv.appendChild(faceDiv);
    rowDiv.appendChild(textDiv);
    
    var cardBodyDiv = document.createElement('div');
    cardBodyDiv.className = 'card-body';
    cardBodyDiv.appendChild(rowDiv);
    
    var cardDiv = document.createElement('div');
    cardDiv.className = 'card';
    cardDiv.setAttribute('id', c.id);
    cardDiv.appendChild(cardBodyDiv);
   
    var newComment = document.createElement('li');
    newComment.setAttribute('rel', c.id);
    newComment.appendChild(cardDiv);

    $('#commentsList').prepend(newComment);
}

function onclickLike(event) {
    alert('test');
}

function comment_vote(e) {
    var target = $(e.target),
        o = target.closest(".js-comment-vote"),
        b = target.closest(".js-comment-btn"),
        i = {
            id: o.attr("data-id"),
            newsId: o.attr("data-news-target"),
            action: b.attr("data-action")
            };

    connection.invoke("VoteAsync", i.id, i.action).catch(function (err) {
        return console.error(err.toString());
    });

}

function clearComment() {
    if (!!document.getElementById("messageInput"))
    {
        document.getElementById("messageInput").value = "";
    }
}

// TODO: add commentviewmodel

function vote(id, action) {
    if (!id) return;
    var target = $('#commentsList').find('#' + id);
    if (!target)
    {
        console('can not find comment with id=' + id);
        return;
    }
    o = target.find('.js-comment-vote');
    o.find('.js-comment-like').prop( "disabled", (action == 'like'));
    o.find('.js-comment-dislike').prop( "disabled", !(action == 'like'));
}