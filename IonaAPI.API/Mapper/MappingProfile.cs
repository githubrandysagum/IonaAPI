using AutoMapper;
using IonaAPI.Core.Models;
using IonaAPI.Dtos;

namespace IonaAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Breed, BreedDto>();
            CreateMap<BreedImage, BreedImageDto>();
            CreateMap<BreedImages, BreedImagesDto>();
            CreateMap<Images, ImagesDto>();
            CreateMap<Image, ImageDto>();
        }
    }
}
