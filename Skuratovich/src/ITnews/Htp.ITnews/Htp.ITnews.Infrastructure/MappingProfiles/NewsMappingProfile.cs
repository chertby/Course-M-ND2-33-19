using System;
using System.Collections.Generic;
using System.Linq;
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
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Author.Id))
                .ForMember(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.UpdatedById, opt => opt.MapFrom(src => src.UpdatedBy == null ? new Guid() : src.UpdatedBy.Id))
                .ForMember(dest => dest.UpdatedByUserName, opt => opt.MapFrom(src => src.UpdatedBy == null ? "" : src.UpdatedBy.UserName))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.NewsTags))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.CommentCount))

                //.ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings))
                //.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Ratings.Any(r => r.AppUserId == null).Average(x => x.Value)))
                //.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Ratings == null ? 0 : (decimal)src.Ratings.Average(x => x.Value)))
                //.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Ratings == null ? 0 : (decimal)src.Ratings.Count))
                //.AfterMap((src, dest) => dest.Rating = src.Ratings.Average())
                .ForAllOtherMembers(opt => opt.Ignore());
        }

        private void MapNewsViewModelToNews()
        {
            CreateMap<NewsViewModel, News>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                //.ForPath(dest => dest.Category.Id, opt => opt.MapFrom(src => src.CategoryId))
                //.ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                //.ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
