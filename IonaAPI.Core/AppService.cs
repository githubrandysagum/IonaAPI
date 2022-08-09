using IonaAPI.Core.Services.Common;
using IonaAPI.Core.Models;
using IonaAPI.Core.Services.Queries;
using IonaAPI.Core.Interfaces;
using IonaAPI.Core.ApiResult;
using IonaAPI.Core.Queries;

namespace IonaAPI.Services
{
    public class AppService : IAppService
    {
        private readonly ICatService catService;
        private readonly IDogService dogService;

        public AppService(ICatService catService, IDogService dogService)
        {
            this.catService = catService;
            this.dogService = dogService;
        }
        
        public async Task<PageListResult<Breed>> GetBreedsAsync(int page = 0, int limit = 10)
        {
            var catQuery = new GetCatClientBreedsQuery(catService);
            var dogQuery = new GetDogClientBreedsQuery(dogService);

            return await GetFromCombinedClientQueryAsync(catQuery, dogQuery, page, limit);

        }

        public async Task<PageListResult<BreedImages>> GetImagesByBreedIdAsync(string breedId, int page = 0, int limit = 10)
        {
            var catQuery = new GetCatClientImagesByBreedIdQuery(catService, breedId);
            var dogQuery = new GetDogClientImagesByBreedIdQuery(dogService, breedId);
            return await GetFromCombinedClientQueryAsync(catQuery, dogQuery, page, limit);

        }

        public async Task<PageListResult<Images>> GetImagesAsync(int page = 0, int limit = 10)
        {
            var catQuery = new GetCatClientImagesQuery(catService);
            var dogQuery = new GetDogClientImagesQuery(dogService);
            return await GetFromCombinedClientQueryAsync(catQuery, dogQuery, page, limit);


        }
        public async Task<Image> GetImageByIdAsync(string imageId)
        {
            var result = await catService.GetImageByIdAsync(imageId);
            if(result == null)
            {
                result = await dogService.GetImageByIdAsync(imageId);
            }
          
            return result;
        }


        private async Task<PageListResult<T>> GetFromCombinedClientQueryAsync<T>(IPageListQuery<T> catQuery, IPageListQuery<T> dogQuery, int page, int limit)
        {
            var list = new PageListResult<T>(page, limit);

            var catList = await catQuery.ExecuteAsync(page, limit);
            list.AddRange(catList);
            //If Cat list has not enough data for the page. Add dog data

            if (catList.ResultCount < limit && catList.ResultCount > 0)
            {
                var take = limit - catList.ResultCount;

                //Add part of dog data
                list.AddRangeTake(await dogQuery.ExecuteAsync(0, limit), take);
            }

            //If Cat list could not provide data for the page. Add dog data
            if (catList.ResultCount == 0)
            {
                var toSkip = limit - (catList.PageCount % limit);
                var toSkipPage = (catList.PageCount / limit);
                var toGetPage = (page - toSkipPage) - 1;

                //Add First part of dog data
                list.AddRangeSkip(await dogQuery.ExecuteAsync(toGetPage, limit), toSkip);

                if (toSkipPage > 0)
                {
                    //Add Second part of dog data
                    list.AddRangeTake(await dogQuery.ExecuteAsync(toGetPage + 1, limit), toSkip);
                }
            }
            return list;
        }
    }
}
