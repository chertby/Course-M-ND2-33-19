using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Domain.Contracts
{
    public interface ITagService
    {
        Task<TagViewModel> GetAsync(Guid id);
        Task<IEnumerable<TagViewModel>> GetTagsByTermAsync(string term);
        Task<IEnumerable<TagForCloudViewModel>> GetTagsForCloudAsync();
    }
}
