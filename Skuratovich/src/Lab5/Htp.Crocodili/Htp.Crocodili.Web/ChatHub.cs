using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.Crocodili.Web.Models;
using Htp.Crocodili.Web.Servicies;
using Microsoft.AspNetCore.SignalR;

namespace Htp.Crocodili.Web
{
    public class ChatHub : Hub
    {
        private readonly IChatRoomService chatRoomService;

        public ChatHub(IChatRoomService chatRoomService)
        {
            this.chatRoomService = chatRoomService;
        }

        public override async Task OnConnectedAsync()
        {
            await chatRoomService.AddConnectionId(Context.ConnectionId);

            await LoadHistory();

            await Clients.Caller.SendAsync(
                "ReceiveMessage",
                "Crocodili",
                DateTimeOffset.UtcNow,
                "Let's play!");

            await Info();

            var connections = await chatRoomService.GetConnections();
            var isGameStarted = await chatRoomService.IsGameStarted();

            if (!isGameStarted)
            {
                if (connections.Count() >= 2)
                {
                    await StartGame();
                }
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await chatRoomService.RemoveConnectionId(Context.ConnectionId);

            await Info();

            var connections = await chatRoomService.GetConnections();
            var isGameStarted = await chatRoomService.IsGameStarted();

            if (isGameStarted)
            {
                if (connections.Count() < 2)
                {
                    await StopGame();
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string name, string text)
        {
            var message = new ChatMessage
            {
                SenderName = name,
                Text = text,
                SentAt = DateTimeOffset.UtcNow
            };

            await chatRoomService.AddMessage(message);

            // Broadcast to all clients
            await Clients.All.SendAsync(
                "ReceiveMessage",
                message.SenderName,
                message.SentAt,
                message.Text);

            var isGameStarted = await chatRoomService.IsGameStarted();
            if (isGameStarted)
            {
                var currentWord = await chatRoomService.GetCurrentWord();

                if (currentWord == text)
                {
                    message = new ChatMessage
                    {
                        SenderName = "Crocodili",
                        Text = $"{name} win!",
                        SentAt = DateTimeOffset.UtcNow
                    };

                    await chatRoomService.AddMessage(message);

                    // Broadcast to all clients
                    await Clients.All.SendAsync(
                        "ReceiveMessage",
                        message.SenderName,
                        message.SentAt,
                        message.Text);

                    await StopGame();

                    await StartGame();

                }
            }
        }

        public async Task SendLine(int moveToX, int moveToY, int lineToX, int lineToY)
        {
            var line = new Line
            {
                MoveToX = moveToX,
                MoveToY = moveToY,
                LineToX = lineToX,
                LineToY = lineToY
            };

            await chatRoomService.AddLine(line);

            await Clients.Others.SendAsync(
                "DrawLine",
                line.MoveToX,
                line.MoveToY,
                line.LineToX,
                line.LineToY
                );
        }

        public async Task LoadHistory()
        {
            var history = await chatRoomService.GetMessageHistory();

            await Clients.Caller.SendAsync("ReceiveMessages", history);

            var lineHistory = await chatRoomService.GetLineHistory();

            await Clients.Caller.SendAsync("DrawLines", lineHistory);

        }

        private async Task Info(string text)
        {
            await SendMessage("Crocodili", text);
        }

        private async Task Info()
        {
            var connections = await chatRoomService.GetConnections();

            await Clients.All.SendAsync(
                "ReceiveMessage",
                "Crocodili",
                DateTimeOffset.UtcNow,
                $"Now {connections.Count()} connection(s)");
        }

        public async Task DisableCanvas(bool disabled)
        {
            var narratorId = await chatRoomService.GetNarratorId();
            await Clients.Client(narratorId).SendAsync(
                "DisableCanvas",
                disabled);
        }

        public async Task GameStarted()
        {
            var narratorId = await chatRoomService.GetNarratorId();
            await Clients.Client(narratorId).SendAsync(
                "GameStarted");
        }

        public async Task GameStopped()
        {
            var narratorId = await chatRoomService.GetNarratorId();
            await Clients.Client(narratorId).SendAsync(
                "GameStopped");
        }

        public async Task ClearCanvas()
        {
            await chatRoomService.ClearLineHostory();
            await Clients.All.SendAsync(
                "ClearCanvas");
        }

        private async Task StartGame()
        {
            await Info("Game started!..");

            await chatRoomService.StartGame();

            var connections = await chatRoomService.GetConnections();

            Random rand = new Random();

            var narratorIndex = rand.Next(connections.Count());

            var narratorId = connections.ElementAt(narratorIndex);

            await chatRoomService.SetNarratorId(narratorId);

            var words = await chatRoomService.GetWords();

            var wordIndex = rand.Next(words.Count());

            var word = words.ElementAt(wordIndex);

            await chatRoomService.SetCurrentWord(word);

            await Clients.Client(narratorId).SendAsync(
               "ReceiveMessage",
               "Crocodili",
               DateTimeOffset.UtcNow,
               $"You are narrator! Draw '{word}'!");

            //await Clients.Client(narratorId).SendAsync(
            //"ReceiveMessage",
            //"Crocodili",
            //DateTimeOffset.UtcNow,
            //$"You are narrator!");

            //await Info($"Narrator is {connections.ElementAt(narratorIndex)}");

            await ClearCanvas();

            await GameStarted();
        }

        private async Task StopGame()
        {
            await Info("Game stopped!..");

            await GameStopped();

            await chatRoomService.SetNarratorId("");

            await chatRoomService.SetCurrentWord("");

            await chatRoomService.StopGame();
        }
    }
}
