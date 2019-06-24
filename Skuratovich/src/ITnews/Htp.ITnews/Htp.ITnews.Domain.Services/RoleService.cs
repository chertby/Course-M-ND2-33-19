using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Domain.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IMapper mapper;

        public RoleService(RoleManager<IdentityRole<Guid>> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public Task<IEnumerable<RoleViewModel>> GetRolesAsync()
        {
            var roles = roleManager.Roles.ToList();
            var result = mapper.Map<IEnumerable<RoleViewModel>>(roles);
            return Task.FromResult(result);
        }
    }
}
