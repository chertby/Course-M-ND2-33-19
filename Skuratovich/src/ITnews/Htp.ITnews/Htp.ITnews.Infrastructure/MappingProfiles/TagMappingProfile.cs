using AutoMapper;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Infrastructure.MappingProfiles
{
    public class TagMappingProfile : Profile
    {

        public TagMappingProfile()
        {
            MapTagToTagViewModel();
        }

        private void MapTagToTagViewModel()
        {
            CreateMap<Tag, TagViewModel>()
                .ForMember(dest => dest.Value, c => c.MapFrom(src => src.Title))
                .ForMember(dest => dest.Label, c => c.MapFrom(src => src.Title))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}
