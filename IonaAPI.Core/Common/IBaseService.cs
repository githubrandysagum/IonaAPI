
using IonaAPI.Core.ApiResult;
using IonaAPI.Core.Models;

namespace IonaAPI.Core.Common
{
    public interface IBaseService
    {
        public Task<PageListCountResult<Breed>> GetBreedsAsync(int page = 0, int limit = 10);

        public Task<PageListCountResult<BreedImages>> GetImagesByBreedIdAsync(string breedId, int page = 0, int limit = 10);

        public Task<PageListCountResult<Images>> GetImagesAsync(int page = 0, int limit = 10);

        public Task<Image> GetImageByIdAsync(string imageId);
    }
}
