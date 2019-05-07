using AutoMapper;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Infrastructure.MappingProfiles
{
    public class NewsMappingProfile : Profile
    {
        public NewsMappingProfile()
        {
            MapNewsToNewsViewModel();
            MapNewsViewModelToNews();
        }

        private void MapNewsToNewsViewModel()
        {
            CreateMap<News, NewsViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.CategoryTitle, opt => opt.MapFrom(src => src.Category.Title))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

        private void MapNewsViewModelToNews()
        {
            CreateMap<NewsViewModel, News>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                //.ForPath(dest => dest.Category.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
