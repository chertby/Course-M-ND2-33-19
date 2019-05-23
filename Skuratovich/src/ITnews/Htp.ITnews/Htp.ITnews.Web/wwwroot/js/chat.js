// Initialize the SignalR client
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on('ReceiveComment', addComment);
connection.on('ReceiveComments', addComments);

connection.start().then(function(){
    var newsId = document.getElementById("NewsViewModel_Id").value;
    connection.invoke("AddToGroup", newsId).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    //var name = document.getElementById("userInput").value;
    var newsId = document.getElementById("NewsViewModel_Id").value;
    var content = document.getElementById("messageInput").value;
    connection.invoke("SendComment", newsId, content).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function addComments(messages) {
    if (!messages) return;s
    messages.forEach(function (m) {
        addComment(m.senderName, m.sentAt, m.text);
    });
}

function addComment(id, authorUserName, content, created) {
    var authorSpan = document.createElement('span');
    authorSpan.className = 'user-info_nickname';
    authorSpan.textContent = authorUserName;

    var authorA = document.createElement('a');
    authorA.className = 'user-info';
    authorA.appendChild(authorSpan);

    var commentTime = document.createElement('time');
    commentTime.className = 'comment__date-time';
    commentTime.textContent = moment(created).format('LLL');

    var likeButton = document.createElement('div');
    likeButton.className = 'btn comment__like';

    var dislikeButton = document.createElement('div');
    dislikeButton.className = 'btn comment__dislike';

    var commenLikeDiv = document.createElement('div');
    commenLikeDiv.className = 'comment__like';
    commenLikeDiv.appendChild(likeButton);
    commenLikeDiv.appendChild(dislikeButton);

    var commentHeaderDiv = document.createElement('div');
    commentHeaderDiv.className = 'comment__header';
    commentHeaderDiv.appendChild(authorA);
    commentHeaderDiv.appendChild(commentTime);

    var commentMessageDiv = document.createElement('div');
    commentMessageDiv.className = 'comment__message';
    commentMessageDiv.textContent = content.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var commenFooterDiv = document.createElement('div');
    commentHeaderDiv.className = 'comment__footer';

    var commentDiv = document.createElement('div');
    commentDiv.className = 'comment';
    commentDiv.setAttribute('id', id);
    commentDiv.appendChild(commentHeaderDiv);
    commentDiv.appendChild(commentMessageDiv);
    commentDiv.appendChild(commenFooterDiv);
   
    var newComment = document.createElement('li');
    newComment.setAttribute('rel', id);
    newComment.appendChild(commentDiv);

    var commentsListUl = document.getElementById('commentsList');
    commentsListUl.appendChild(newComment);
    commentsListUl.scrollTop = commentsListUl.scrollHeight - commentsListUl.clientHeight;
}