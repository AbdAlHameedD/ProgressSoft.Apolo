using AutoMapper;
using ProgressSoft.Apolo.Application.DTOs;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Application;

public class BusinessCardMappingProfile : Profile
{
    public BusinessCardMappingProfile()
    {
        AllowNullCollections = true;

        CreateMap<BusinessCard, BusinessCardModel>()
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.Ignore());

        CreateMap<AddBusinessCardCommand, BusinessCard>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0));

        CreateMap<ImportBusinessCard, BusinessCardModel>()
            .ForMember(dest => dest.ImageId, opt => opt.MapFrom(src => src.Image == null || src.ImageType == null ? 1 : -1));
    }
}
