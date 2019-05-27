using System.Collections.Generic;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Domain.Contracts
{
    public interface ITagService
    {
        Task<IEnumerable<TagViewModel>> GetTagsByTermAsync(string term);
        Task<int> Test();
    }
}
