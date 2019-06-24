using AutoMapper;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Infrastructure.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            MapAppUserToUserViewModel();
            MapUserViewModelToAppUser();
        }

        private void MapAppUserToUserViewModel()
        {
            CreateMap<AppUser, UserViewModel>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.AccessFailedCount, c => c.MapFrom(src => src.AccessFailedCount))
                .ForMember(dest => dest.ConcurrencyStamp, c => c.MapFrom(src => src.ConcurrencyStamp))
                .ForMember(dest => dest.Email, c => c.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmailConfirmed, c => c.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.LockoutEnabled, c => c.MapFrom(src => src.LockoutEnabled))
                .ForMember(dest => dest.LockoutEnd, c => c.MapFrom(src => src.LockoutEnd))
                .ForMember(dest => dest.NormalizedEmail, c => c.MapFrom(src => src.NormalizedEmail))
                .ForMember(dest => dest.NormalizedUserName, c => c.MapFrom(src => src.NormalizedUserName))
                .ForMember(dest => dest.PasswordHash, c => c.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.PhoneNumber, c => c.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PhoneNumberConfirmed, c => c.MapFrom(src => src.PhoneNumberConfirmed))
                .ForMember(dest => dest.SecurityStamp, c => c.MapFrom(src => src.SecurityStamp))
                .ForMember(dest => dest.TwoFactorEnabled, c => c.MapFrom(src => src.TwoFactorEnabled))
                .ForMember(dest => dest.UserName, c => c.MapFrom(src => src.UserName))
                .ForMember(dest => dest.FirstName, c => c.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, c => c.MapFrom(src => src.LastName))
                .ForMember(dest => dest.News, c => c.MapFrom(src => src.News))
                .ForMember(dest => dest.IsActive, c => c.MapFrom(src => src.IsActive))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapUserViewModelToAppUser()
        {
            CreateMap<UserViewModel, AppUser>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.AccessFailedCount, c => c.MapFrom(src => src.AccessFailedCount))
                .ForMember(dest => dest.ConcurrencyStamp, c => c.MapFrom(src => src.ConcurrencyStamp))
                .ForMember(dest => dest.Email, c => c.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmailConfirmed, c => c.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.LockoutEnabled, c => c.MapFrom(src => src.LockoutEnabled))
                .ForMember(dest => dest.LockoutEnd, c => c.MapFrom(src => src.LockoutEnd))
                .ForMember(dest => dest.NormalizedEmail, c => c.MapFrom(src => src.NormalizedEmail))
                .ForMember(dest => dest.NormalizedUserName, c => c.MapFrom(src => src.NormalizedUserName))
                .ForMember(dest => dest.PasswordHash, c => c.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.PhoneNumber, c => c.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PhoneNumberConfirmed, c => c.MapFrom(src => src.PhoneNumberConfirmed))
                .ForMember(dest => dest.SecurityStamp, c => c.MapFrom(src => src.SecurityStamp))
                .ForMember(dest => dest.TwoFactorEnabled, c => c.MapFrom(src => src.TwoFactorEnabled))
                .ForMember(dest => dest.UserName, c => c.MapFrom(src => src.UserName))
                .ForMember(dest => dest.FirstName, c => c.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, c => c.MapFrom(src => src.LastName))
                .ForMember(dest => dest.IsActive, c => c.MapFrom(src => src.IsActive))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}
