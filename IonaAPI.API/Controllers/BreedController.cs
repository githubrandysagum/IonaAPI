using IonaAPI.Core.Models;
using IonaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using IonaAPI.Dtos;
using AutoMapper;
using IonaAPI.Core.Interfaces;
using IonaAPI.Core.ApiResult;

namespace IonaAPI.Controllers
{
    [ApiController]
    public class BreedsController : ControllerBase
    {
        private IAppService appService;
        private IMapper mapper;

        public BreedsController(IAppService appService, IMapper mapper)
        {
            this.appService = appService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("Breeds")]
        public async Task<PageListResult<BreedDto>> GetBreedsAsync([FromQuery] int page, [FromQuery] int limit)
        {
            var result = await appService.GetBreedsAsync(page, limit);
            var list = mapper.Map<List<Breed>, List<BreedDto>>(result.Results);
            return new PageListResult<BreedDto>(page, limit, list);
        }

        [HttpGet]
        [Route("Breeds/{id}")]
        public async Task<PageListResult<BreedImagesDto>> GetBreedByIdAsync(string id, [FromQuery] int page, [FromQuery] int limit)
        {
            var result = await appService.GetImagesByBreedIdAsync(id, page, limit);
            var list = mapper.Map<List<BreedImages>, List<BreedImagesDto>>(result.Results);
            return new PageListResult<BreedImagesDto>(page, limit, list);
        }

        [HttpGet]
        [Route("list")]
        public async Task<PageListResult<ImagesDto>> GetImagesAsync([FromQuery] int page, [FromQuery] int limit)
        {
            var result = await appService.GetImagesAsync(page, limit);
            var list = mapper.Map<List<Images>, List<ImagesDto>>(result.Results);
            return new PageListResult<ImagesDto>(page, limit, list);
        }

        [HttpGet]
        [Route("Image/{id}")]
        public async Task<ImageDto> GetImageByIdAsync(string id)
        {
            var result = await appService.GetImageByIdAsync(id);
            var image = mapper.Map<Image, ImageDto>(result);
            return image;

        }
    }
}
