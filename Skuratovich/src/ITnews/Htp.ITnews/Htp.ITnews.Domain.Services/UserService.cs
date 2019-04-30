using System;
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
            // TODO: get user from db by Id
            var user = await userManager.FindByIdAsync(userViewModel.Id.ToString());
            //var user = mapper.Map<AppUser>(userViewModel);
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
    }
}
