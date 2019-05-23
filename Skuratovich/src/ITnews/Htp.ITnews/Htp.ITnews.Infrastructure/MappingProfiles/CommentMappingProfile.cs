using System;
using AutoMapper;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Infrastructure.MappingProfiles
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            MapCommentToCommentViewModel();
            MapCommentViewModelToComment();
        }

        private void MapCommentToCommentViewModel()
        {
            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.NewsId, c => c.MapFrom(src => src.News.Id))
                .ForMember(dest => dest.Content, c => c.MapFrom(src => src.Description))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Author.Id))
                .ForMember(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.UpdatedById, opt => opt.MapFrom(src => src.UpdatedBy == null ? new Guid() : src.UpdatedBy.Id))
                .ForMember(dest => dest.UpdatedByUserName, opt => opt.MapFrom(src => src.UpdatedBy == null ? "" : src.UpdatedBy.UserName))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapCommentViewModelToComment()
        {
            CreateMap<CommentViewModel, Comment>()
                .ForMember(dest => dest.Description, c => c.MapFrom(src => src.Content))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}
