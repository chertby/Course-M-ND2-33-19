using System;
using Microsoft.AspNetCore.Identity;

namespace Htp.ITnews.Domain.Contracts.ViewModels
{
    public class RoleViewModel : IdentityRole<Guid>, IViewModel
    {
    }
}
