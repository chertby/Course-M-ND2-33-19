using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Authentication;

namespace Htp.ITnews.Domain.Contracts
{
    public interface ISignInService
    {
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        Boolean IsSignedIn(ClaimsPrincipal principal);
        Task SignInAsync(UserViewModel userViewModel, bool isPersistent);
    }
}
