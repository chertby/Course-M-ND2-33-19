using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Htp.ITnews.Domain.Services
{
    public class SignInService : ISignInService
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly IMapper mapper;

        public SignInService(SignInManager<AppUser> signInManager, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task SignInAsync(UserViewModel userViewModel, bool isPersistent)
        {
            var user = mapper.Map<AppUser>(userViewModel);
            await signInManager.SignInAsync(user, isPersistent: isPersistent);
        }

        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            var result = await signInManager.GetExternalAuthenticationSchemesAsync();
            return result;
        }

        public Boolean IsSignedIn(ClaimsPrincipal principal)
        {
            var result = signInManager.IsSignedIn(principal);
            return result;
        }
    }
}
