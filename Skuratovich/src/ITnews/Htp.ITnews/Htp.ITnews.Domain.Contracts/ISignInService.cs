using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Htp.ITnews.Domain.Contracts
{
    public interface ISignInService
    {
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        Boolean IsSignedIn(ClaimsPrincipal principal);
        Task SignInAsync(UserViewModel userViewModel, bool isPersistent);
        Task RefreshSignInAsync(UserViewModel userViewModel);
        Task<SignInResult> PasswordSignInAsync(String userName, String password, Boolean isPersistent, Boolean lockoutOnFailure);
        Task SignOutAsync();
    }
}
