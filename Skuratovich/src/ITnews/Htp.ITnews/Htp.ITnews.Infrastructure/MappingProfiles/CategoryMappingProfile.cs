using AutoMapper;
using Htp.ITnews.Data.Contracts.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Infrastructure.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            MapCategoryToSelectListItem();
        }

        public void MapCategoryToSelectListItem()
        {
            CreateMap<Category, SelectListItem>()
                .ForMember(dest => dest.Value, c => c.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, c => c.MapFrom(src => src.Title))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}
