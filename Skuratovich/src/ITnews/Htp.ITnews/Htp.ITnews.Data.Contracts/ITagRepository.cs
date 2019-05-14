using System.Collections.Generic;
using Htp.ITnews.Data.Contracts.Entities;

namespace Htp.ITnews.Data.Contracts
{
    public interface ITagRepository : IRepository<Tag>
    {
        IEnumerable<Tag> GetTagsByTerm(string term);
    }
}
