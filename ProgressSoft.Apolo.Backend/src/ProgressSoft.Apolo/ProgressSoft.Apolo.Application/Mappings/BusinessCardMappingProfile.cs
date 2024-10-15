using AutoMapper;
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
    }
}
