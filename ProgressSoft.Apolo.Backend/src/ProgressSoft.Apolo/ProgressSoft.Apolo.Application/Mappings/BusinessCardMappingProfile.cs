using AutoMapper;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Application;

public class BusinessCardMappingProfile : Profile
{
    public BusinessCardMappingProfile()
    {
        AllowNullCollections = true;

        CreateMap<BusinessCard, BusinessCardModel>();

        CreateMap<AddBusinessCardCommand, BusinessCard>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0));
    }
}
