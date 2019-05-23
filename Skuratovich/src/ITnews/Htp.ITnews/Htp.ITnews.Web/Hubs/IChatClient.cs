using System;
using System.Threading.Tasks;

namespace Htp.ITnews.Web.Hubs
{
    public interface IChatClient
    {
        Task ReceiveComment(Guid id, string authorUserName, string content, DateTime created);
    }
}
