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
        }

        private void MapNewsToNewsViewModel()
        {
            CreateMap<News, NewsViewModel>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, c => c.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, c => c.MapFrom(src => src.Description))
                .ForMember(dest => dest.Content, c => c.MapFrom(src => src.Content))

                //.ForMember(dest => dest.MiddleName, c => c.MapFrom(src => src.MiddleName))
                //.ForMember(dest => dest.LastName, c => c.MapFrom(src => src.LastName))
                //.ForMember(dest => dest.Address, c => c.MapFrom(src => src.Address))
                //.ForMember(dest => dest.City, c => c.MapFrom(src => src.City))
                //.ForMember(dest => dest.Country, c => c.MapFrom(src => src.Country))
                //.ForMember(dest => dest.PostCode, c => c.MapFrom(src => src.PostCode))
                //.ForMember(dest => dest.Email, c => c.MapFrom(src => src.Email))
                //.ForMember(dest => dest.Amount, c => c.MapFrom(src => src.Amount))
                //.ForMember(dest => dest.Description, c => c.MapFrom(src => src.Description))
                //.ForMember(dest => dest.CreditCardNumber, c => c.MapFrom(src => src.CreditCardNumber))
                //.ForMember(dest => dest.ExpirationMonth, c => c.MapFrom(src => src.ExpirationMonth))
                //.ForMember(dest => dest.ExpirationYear, c => c.MapFrom(src => src.ExpirationYear))
                .ForAllOtherMembers(c => c.Ignore());
        }

        //private void MapPaymentRequestToPayment()
        //{
        //    CreateMap<CreatePaymentRequest, Payment>()
        //        .ForMember(dest => dest.FirstName, c => c.MapFrom(src => src.FirstName))
        //        .ForMember(dest => dest.MiddleName, c => c.MapFrom(src => src.MiddleName))
        //        .ForMember(dest => dest.LastName, c => c.MapFrom(src => src.LastName))
        //        .ForMember(dest => dest.Address, c => c.MapFrom(src => src.Address))
        //        .ForMember(dest => dest.City, c => c.MapFrom(src => src.City))
        //        .ForMember(dest => dest.Country, c => c.MapFrom(src => src.Country))
        //        .ForMember(dest => dest.PostCode, c => c.MapFrom(src => src.PostCode))
        //        .ForMember(dest => dest.Email, c => c.MapFrom(src => src.Email))
        //        .ForMember(dest => dest.Amount, c => c.MapFrom(src => src.Amount))
        //        .ForMember(dest => dest.Description, c => c.MapFrom(src => src.Description))
        //        .ForMember(dest => dest.CreditCardNumber, c => c.MapFrom(src => src.CreditCardNumber))
        //        .ForMember(dest => dest.ExpirationMonth, c => c.MapFrom(src => src.ExpirationMonth))
        //        .ForMember(dest => dest.ExpirationYear, c => c.MapFrom(src => src.ExpirationYear))
        //        .ForMember(dest => dest.SecurityCode, c => c.MapFrom(src => src.SecurityCode))
        //        .ForAllOtherMembers(c => c.Ignore());
        //}
    }
}
