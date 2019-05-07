using System;
namespace Htp.ITnews.Domain.Contracts
{
    public interface IResource
    {
        Guid AuthorId { get; set; }
    }
}
