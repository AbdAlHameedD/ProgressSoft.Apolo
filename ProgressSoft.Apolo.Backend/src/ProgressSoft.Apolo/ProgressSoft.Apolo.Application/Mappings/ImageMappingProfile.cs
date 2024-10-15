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
                .ForMember(src => src.EncodedImage, opt => opt.MapFrom(dest => Convert.ToBase64String(dest.EncodedImage)))
                .ReverseMap()
                .ForMember(src => src.EncodedImage, opt => opt.MapFrom(dest => Convert.FromBase64String(dest.EncodedImage)));
        }
    }
}
