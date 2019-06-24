using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Htp.Crocodili.Web.Models;

namespace Htp.Crocodili.Web.Servicies
{
    public interface IChatRoomService
    {
        Task AddConnectionId(string connectionId);

        Task RemoveConnectionId(string connectionId);

        Task<IEnumerable<string>> GetConnections();

        Task AddMessage(Guid roomId, ChatMessage message);

        Task<IEnumerable<ChatMessage>> GetMessageHistory(Guid roomId);

        Task AddMessage(ChatMessage message);

        Task<IEnumerable<ChatMessage>> GetMessageHistory();

        Task AddLine(Line line);

        Task<IEnumerable<Line>> GetLineHistory();

        Task<bool> IsGameStarted();

        Task StartGame();

        Task StopGame();

        Task<string> GetNarratorId();

        Task SetNarratorId(string narratorId);

        Task ClearLineHostory();

        Task<IEnumerable<string>> GetWords();

        Task<string> GetCurrentWord();

        Task SetCurrentWord(string word);
    }
}
