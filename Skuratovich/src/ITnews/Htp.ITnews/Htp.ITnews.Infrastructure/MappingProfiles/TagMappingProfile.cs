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
            MapNewsTagToTagViewModel();
        }

        private void MapTagToTagViewModel()
        {
            CreateMap<Tag, TagViewModel>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, c => c.MapFrom(src => src.Title))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapNewsTagToTagViewModel()
        {
            CreateMap<NewsTag, TagViewModel>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.TagId))
                .ForMember(dest => dest.Title, c => c.MapFrom(src => src.Tag.Title))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}
