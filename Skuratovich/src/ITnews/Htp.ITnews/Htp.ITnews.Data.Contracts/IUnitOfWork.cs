using System.Threading.Tasks;

namespace Htp.ITnews.Data.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        ITransaction BeginTransaction();
    }
}
