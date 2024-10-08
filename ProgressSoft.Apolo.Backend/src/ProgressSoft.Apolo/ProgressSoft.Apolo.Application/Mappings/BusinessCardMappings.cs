using System.Text;
using AutoMapper;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Application;

public class BusinessCardMappingProfile : Profile
{
    public BusinessCardMappingProfile()
    {
        AllowNullCollections = true;

        CreateMap<BusinessCard, BusinessCardModel>()
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => (src.Photo != null) ? src.Photo.ToString() : null))
            .ReverseMap()
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => (src.Photo != null) ? Encoding.UTF8.GetBytes(src.Photo) : null));

        CreateMap<AddBusinessCardCommand, BusinessCard>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => (src.Photo != null) ? Encoding.UTF8.GetBytes(src.Photo) : null));
    }
}
