//"use strict";
//var chatterName = 'Visitor';


//Problem: No user interaction capability
//Solution: When user interacts, cause changes appropriately
var color = $(".red").css("background-color");
var $canvas = $("canvas");
var context = $canvas[0].getContext("2d");
var lastEvent;
var mouseDown = false;

var isTest = true;

// Initialize the SignalR client
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on('ReceiveMessage', addMessage);
connection.on('ReceiveMessages', addMessages);
connection.on('DrawLine', drawLine);
connection.on('DrawLines', drawLines);
connection.on('GameStarted', gameStarted);
connection.on('GameStopped', gameStopped);
connection.on('ClearCanvas', clearCanvas);

connection.start().then(function(){
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var name = document.getElementById("userInput").value;
    var text = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", name, text).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function addMessages(messages) {
    if (!messages) return;

    messages.forEach(function (m) {
        addMessage(m.senderName, m.sentAt, m.text);
    });
}

function addMessage(name, time, message) {
    var nameSpan = document.createElement('span');
    nameSpan.className = 'name';
    nameSpan.textContent = name;

    var timeSpan = document.createElement('span');
    timeSpan.className = 'time';
    var friendlyTime = moment(time).format('H:mm');
    timeSpan.textContent = friendlyTime;

    var headerDiv = document.createElement('div');
    headerDiv.appendChild(nameSpan);
    headerDiv.appendChild(timeSpan);

    var messageDiv = document.createElement('div');
    messageDiv.className = 'message';
    messageDiv.textContent = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");;

    var newItem = document.createElement('li');
    newItem.appendChild(headerDiv);
    newItem.appendChild(messageDiv);

    var chatHistoryEl = document.getElementById('chatHistory');
    chatHistoryEl.appendChild(newItem);
    chatHistoryEl.scrollTop = chatHistoryEl.scrollHeight - chatHistoryEl.clientHeight;
}

function drawLines(lines) {
    if (!lines) return;

    lines.forEach(function (l) {
        drawLine(l.moveToX, l.moveToY, l.lineToX, l.lineToY);
    });
}

function drawLine(moveToX, moveToY, lineToX, lineToY) {
    context.beginPath();
    context.moveTo(moveToX, moveToY);
    context.lineTo(lineToX, lineToY);
    context.strokeStyle = 'red';
    context.stroke();
}

function log(message, testEvent)
{
    if ((isTest) && ("offsetX" in testEvent))
    {
        console.log(message + " " + testEvent.offsetX);
    }
 }

function mousedown(e)
{
    lastEvent = e;
    mouseDown = true;
    //log('mousedown: lastEvent.offsetX', lastEvent);
}

function mouseup()
{
    //log('mouseup: lastEvent.offsetX', lastEvent);
    mouseDown = false;
}

function mousemove(e)
{
    //log('mousemove: lastEvent.offsetX', lastEvent);
    //Draw lines
    if(mouseDown) {
        context.beginPath();
        var moveToX = lastEvent.offsetX;
        var moveToY = lastEvent.offsetY;
        var lineToX = e.offsetX;
        var lineToY = e.offsetY;
        context.moveTo(moveToX, moveToY);
        context.lineTo(lineToX, lineToY);
        context.strokeStyle = color;
        context.stroke();
        lastEvent = e;
        connection.invoke("SendLine", moveToX, moveToY, lineToX, lineToY).catch(function (err) {
            return console.error(err.toString());
        });
    }
}

function mouseleave()
{
  $canvas.mouseup();
}

function clearCanvas()
{
    var canvas = document.getElementById("canvas");
    context.clearRect(0, 0, canvas.width, canvas.height);
    context.beginPath();
}

function gameStarted()
{
    $canvas.on('mousedown', mousedown); 
    $canvas.on('mousemove', mousemove); 
    $canvas.on('mouseup', mouseup); 
    $canvas.on('mouseleave', mouseleave);
}

function gameStopped()
{
    $canvas.off('mousedown'); 
    $canvas.off('mousemove'); 
    $canvas.off('mouseup'); 
    $canvas.off('mouseleave'); 
}