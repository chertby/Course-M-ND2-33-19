using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Htp.ITnews.Domain.Contracts
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync(UserViewModel userViewModel, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(UserViewModel userViewModel);
        Task<UserViewModel> GetUserAsync(ClaimsPrincipal principal);
        String GetUserId(ClaimsPrincipal principal);
        Task<String> GetUserNameAsync(UserViewModel userViewModel);
        Task<String> GetEmailAsync(UserViewModel userViewModel);
        Task<String> GetPhoneNumberAsync(UserViewModel userViewModel);
        Task<Boolean> IsEmailConfirmedAsync(UserViewModel userViewModel);
        Task<IdentityResult> SetEmailAsync(UserViewModel userViewModel, String emai);
        Task<String> GetUserIdAsync(UserViewModel userViewModel);
        Task<IdentityResult> SetPhoneNumberAsync(UserViewModel userViewModel, String phoneNumber);
        Task<IdentityResult> UpdateAsync(UserViewModel userViewModel);
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<UserViewModel> FindByIdAsync(Guid id);
        Task<IEnumerable<string>> GetRolesAsync(UserViewModel userViewModel);
        Task<IdentityResult> AddToRolesAsync(UserViewModel userViewModel, IEnumerable<string> roles);
        Task<IdentityResult> RemoveFromRolesAsync(UserViewModel userViewModel, IEnumerable<string> roles);
    }
}
