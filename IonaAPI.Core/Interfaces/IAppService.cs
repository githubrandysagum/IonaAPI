using IonaAPI.Core.Models;
using IonaAPI.Core.ApiResult;

namespace IonaAPI.Core.Interfaces
{
    public interface IAppService 
    {
        /// <summary>Gets list of breeds</summary>
        /// <param name="page">Page to return</param>
        /// <param name="limit">limit or page size</param>
        /// <returns>List of breeds</returns>
        public Task<PageListResult<Breed>> GetBreedsAsync(int page = 0, int limit = 10);

        /// <summary>Gets list of breeds images</summary>
        /// <param name="breedId">Id of breed</param>
        /// <param name="page">Page to return</param>
        /// <param name="limit">limit or page size</param>
        /// <returns>List of images by breed</returns>
        public Task<PageListResult<BreedImages>> GetImagesByBreedIdAsync(string breedId, int page = 0, int limit = 10);

        /// <summary>Gets list of images</summary>
        /// <param name="page">Page to return</param>
        /// <param name="limit">limit or page size</param>
        /// <returns>List of images</returns>
        public Task<PageListResult<Images>> GetImagesAsync(int page = 0, int limit = 10);

        /// <summary>Get image by id</summary>
        /// <param name="imageId">Id of the image</param>
        /// <returns>Image</returns>
        public Task<Image> GetImageByIdAsync(string imageId);
    }
}
