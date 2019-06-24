using AutoMapper;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Infrastructure.MappingProfiles
{
    public class RoleMappingProfile : Profile
    {

        public RoleMappingProfile()
        {
            MapIdentityRoleToRoleViewModel();
            MapRoleViewModelToIdentityRole();
            MapIdentityRoleToSelectListItem();
        }

        private void MapIdentityRoleToRoleViewModel()
        {
            CreateMap<IdentityRole, RoleViewModel>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, c => c.MapFrom(src => src.Name))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapRoleViewModelToIdentityRole()
        {
            CreateMap<RoleViewModel, IdentityRole>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, c => c.MapFrom(src => src.Name))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapIdentityRoleToSelectListItem()
        {
            CreateMap<IdentityRole, SelectListItem>()
                .ForMember(dest => dest.Value, c => c.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, c => c.MapFrom(src => src.Name))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}
