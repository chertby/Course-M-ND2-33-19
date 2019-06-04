using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace Htp.ITnews.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IdentityResult> CreateAsync(UserViewModel userViewModel, string password)
        {
            var user = mapper.Map<AppUser>(userViewModel);
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                mapper.Map(user, userViewModel);
            }
            return result;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(UserViewModel userViewModel)
        {
            var user = await userManager.FindByIdAsync(userViewModel.Id.ToString());
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async Task<UserViewModel> GetUserAsync(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            var userViewModel = mapper.Map<UserViewModel>(user);
            return userViewModel;
        }

        public String GetUserId(ClaimsPrincipal principal)
        {
            var result = userManager.GetUserId(principal);
            return result;
        }

        public async Task<String> GetUserNameAsync(UserViewModel userViewModel)
        {
            // TODO: get user from database
            var user = mapper.Map<AppUser>(userViewModel);
            var result = await userManager.GetUserNameAsync(user);
            return result;
        }

        public async Task<String> GetEmailAsync(UserViewModel userViewModel)
        {
            // TODO: get user from database
            var user = mapper.Map<AppUser>(userViewModel);
            var result = await userManager.GetEmailAsync(user);
            return result;
        }

        public async Task<String> GetPhoneNumberAsync(UserViewModel userViewModel)
        {
            // TODO: get user from database
            var user = mapper.Map<AppUser>(userViewModel);
            var result = await userManager.GetPhoneNumberAsync(user);
            return result;
        }

        public async Task<Boolean> IsEmailConfirmedAsync(UserViewModel userViewModel)
        {
            // TODO: get user from database
            var user = mapper.Map<AppUser>(userViewModel);
            var result = await userManager.IsEmailConfirmedAsync(user);
            return result;
        }

        public async Task<IdentityResult> SetEmailAsync(UserViewModel userViewModel, String emai)
        {
            var user = mapper.Map<AppUser>(userViewModel);
            var result = await userManager.SetEmailAsync(user, emai);
            return result;
        }

        public async Task<String> GetUserIdAsync(UserViewModel userViewModel)
        {
            var user = mapper.Map<AppUser>(userViewModel);
            var result = await userManager.GetUserIdAsync(user);
            return result;
        }

        public async Task<IdentityResult> SetPhoneNumberAsync(UserViewModel userViewModel, String phoneNumber)
        {
            var user = mapper.Map<AppUser>(userViewModel);
            var result = await userManager.SetPhoneNumberAsync(user, phoneNumber);
            return result;
        }

        public async Task<IdentityResult> UpdateAsync(UserViewModel userViewModel)
        {
            var user = await userManager.FindByIdAsync(userViewModel.Id.ToString());
            mapper.Map(userViewModel, user);
            var result = await userManager.UpdateAsync(user);
            return result;
        }

        public Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            var users = userManager.Users.ToList();
            var result = mapper.Map<IEnumerable<UserViewModel>>(users);
            return Task.FromResult(result);
        }

        public async Task<UserViewModel> FindByIdAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            var result = mapper.Map<UserViewModel>(user);
            return result;
        }

        public async Task<IEnumerable<string>> GetRolesAsync(UserViewModel userViewModel)
        {
            var user = await userManager.FindByIdAsync(userViewModel.Id.ToString());
            var result = await userManager.GetRolesAsync(user);
            return result;
        }

        public async Task<IdentityResult> AddToRolesAsync(UserViewModel userViewModel, IEnumerable<string> roles)
        {
            var user = await userManager.FindByIdAsync(userViewModel.Id.ToString());
            var result = await userManager.AddToRolesAsync(user, roles);
            return result;
        }

        public async Task<IdentityResult> RemoveFromRolesAsync(UserViewModel userViewModel, IEnumerable<string> roles)
        {
            var user = await userManager.FindByIdAsync(userViewModel.Id.ToString());
            var result = await userManager.RemoveFromRolesAsync(user, roles);
            return result;
        }

        public async Task<IdentityResult> AddClaimAsync(UserViewModel userViewModel, Claim claim)
        {
            var user = await userManager.FindByIdAsync(userViewModel.Id.ToString());
            var result = await userManager.AddClaimAsync(user, claim);
            return result;
        }

        public async Task<IdentityResult> RemoveClaimAsync(UserViewModel userViewModel, Claim claim)
        {
            var user = await userManager.FindByIdAsync(userViewModel.Id.ToString());
            var result = await userManager.RemoveClaimAsync(user, claim);
            return result;
        }

        public async Task<IdentityResult> RemoveClaimAsync(UserViewModel userViewModel, Claim claim, Claim newClaim)
        {
            var user = await userManager.FindByIdAsync(userViewModel.Id.ToString());
            var result = await userManager.ReplaceClaimAsync(user, claim, newClaim);
            return result;
        }
    }
}
