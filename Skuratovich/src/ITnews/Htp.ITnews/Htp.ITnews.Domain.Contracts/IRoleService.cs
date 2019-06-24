using System.Collections.Generic;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Domain.Contracts
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>> GetRolesAsync();
    }
}
