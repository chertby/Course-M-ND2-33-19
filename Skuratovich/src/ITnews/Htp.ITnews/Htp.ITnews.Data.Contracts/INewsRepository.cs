using System.Collections.Generic;
using System.Threading.Tasks;
using Htp.ITnews.Data.Contracts.Entities;

namespace Htp.ITnews.Data.Contracts
{
    public interface INewsRepository : IRepository<News>
    {
        Task<IList<string>> GetTagsAsync(News news);
        Task AddToTagAsync(News news, string tagName);
    }
}
