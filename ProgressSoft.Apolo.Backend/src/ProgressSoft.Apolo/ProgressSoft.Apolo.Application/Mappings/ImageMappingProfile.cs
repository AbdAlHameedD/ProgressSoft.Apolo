using AutoMapper;
using ProgressSoft.Apolo.Application.Models;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Application.Mappings
{
    public class ImageMappingProfile : Profile
    {
        public ImageMappingProfile()
        {
            AllowNullCollections = true;

            CreateMap<Image, ImageModel>()
                .ForMember(src => src.BusinessCards, opt => opt.MapFrom(dest => dest.BusinessCards))
                .ReverseMap();
        }
    }
}
