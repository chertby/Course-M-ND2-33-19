using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.Crocodili.Web.Models;

namespace Htp.Crocodili.Web.Servicies
{
    public class InMemoryChatRoomService : IChatRoomService
    {
        private readonly Dictionary<Guid, List<ChatMessage>>
            messageHistory = new Dictionary<Guid, List<ChatMessage>>();

        private readonly List<ChatMessage> chatMessages = new List<ChatMessage>();

        private readonly List<Line> lines = new List<Line>();

        private readonly List<string> connectionIds = new List<string>();

        private readonly List<string> words = 
            new List<string>(new string[] { "fox", "dog", "cat" });

        private bool isGameStarted;

        private string narratorId;

        private string word;

        public Task AddConnectionId(string connectionId)
        {
            connectionIds.Add(connectionId);

            return Task.CompletedTask;
        }

        public Task AddLine(Line line)
        {
            lines.Add(line);
            return Task.CompletedTask;
        }

        public Task AddMessage(Guid roomId, ChatMessage message)
        {
            if (!messageHistory.ContainsKey(roomId))
            {
                messageHistory[roomId] = new List<ChatMessage>();
            }

            try
            {
                messageHistory[roomId].Add(message);
            }
            catch(Exception ex)
            {
                var test = ex; 
            }

            return Task.CompletedTask;
        }

        public Task AddMessage(ChatMessage message)
        {
            chatMessages.Add(message);
            return Task.CompletedTask;
        }

        public Task ClearLineHostory()
        {
            lines.Clear();
            return Task.CompletedTask;
        }

        public Task<IEnumerable<string>> GetConnections()
        {
            return Task.FromResult(connectionIds.AsEnumerable());
        }

        public Task<string> GetCurrentWord()
        {
            return Task.FromResult(word);
        }

        public Task<IEnumerable<Line>> GetLineHistory()
        {
            var result = lines
                .AsEnumerable();
            return Task.FromResult(result);
        }

        public Task<IEnumerable<ChatMessage>> GetMessageHistory(Guid roomId)
        {
            messageHistory.TryGetValue(roomId, out var messages);

            messages = messages ?? new List<ChatMessage>();
            var sortedMessages = messages
                .OrderBy(x => x.SentAt)
                .AsEnumerable();

            return Task.FromResult(sortedMessages);
        }

        public Task<IEnumerable<ChatMessage>> GetMessageHistory()
        {
            var sortedMessages = chatMessages
               .OrderBy(x => x.SentAt)
               .AsEnumerable();

            return Task.FromResult(sortedMessages);
        }

        public Task<string> GetNarratorId()
        {
            return Task.FromResult(narratorId);
        }

        public Task<IEnumerable<string>> GetWords()
        {
            return Task.FromResult(words.AsEnumerable());
        }

        public Task<bool> IsGameStarted()
        {
            return Task.FromResult(isGameStarted);
        }

        public Task RemoveConnectionId(string connectionId)
        {
            connectionIds.Remove(connectionId);
            return Task.CompletedTask;
        }

        public Task SetCurrentWord(string word)
        {
            this.word = word;
            return Task.CompletedTask;
        }

        public Task SetNarratorId(string narratorId)
        {
            this.narratorId = narratorId;
            return Task.CompletedTask;
        }

        public Task StartGame()
        {
            isGameStarted = true;

            return Task.CompletedTask;
        }

        public Task StopGame()
        {
            isGameStarted = false;

            return Task.CompletedTask;
        }
    }
}
