using System;
using AutoMapper;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Infrastructure.MappingProfiles
{
    public class RatingMappingProfile : Profile
    {
        public RatingMappingProfile()
        {
            MapRatingToRatingViewModel();
        }

        private void MapRatingToRatingViewModel()
        {
            CreateMap<Rating, RatingViewModel>()
                .ForMember(dest => dest.NewsId, c => c.MapFrom(src => src.NewsId))
                .ForMember(dest => dest.UserId, c => c.MapFrom(src => src.AppUserId))
                .ForMember(dest => dest.Value, c => c.MapFrom(src => src.Value))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}
