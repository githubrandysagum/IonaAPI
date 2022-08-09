using IonaAPI.Core.Services.Common;
using IonaAPI.Core.Models;
using IonaAPI.Infrastructure.Services.Extensions;
using IonaAPI.Infrastructure.Services.Result;
using IonaAPI.Services;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web.Http;
using IonaAPI.Core.Common;
using IonaAPI.Core.ApiResult;

namespace IonaAPI.Infrastructure
{
    public class BaseService : IBaseService
    {
        public HttpClient Client { get; set; }
        public BaseService(HttpClient client)
        {
            Client = client;
        }

        public virtual async Task<PageListCountResult<Breed>> GetBreedsAsync(int page = 0, int limit = 0)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/Breeds?page={page}&limit={limit}");
            var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            var list = new PageListCountResult<Breed>(page, limit);
           
            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync();
                var breeds = stream.ReadAndDeserializeFromJson<List<BreedResult>>();

                list.PageCount = GetCount(response);

                foreach (var breed in breeds)
                {

                    BreedImage image = null;
                    if(breed.Image != null)
                    {
                        image = new BreedImage
                        {
                            Id = breed.Image?.Id,
                            Width = breed.Image.Width,
                            Height = breed.Image.Height,
                            Url = breed.Image.Url,
                        };
                    }

                    list.Results.Add(new Breed
                    {
                        Id = breed.Id,
                        Name = breed.Name,
                        Temperament = breed.Temperament,
                        Origin = breed.Origin,
                        CountryCode = breed.CountryCode,
                        Description = breed.Description,
                        Image = image,
                    });
                }

                
                
            }
            else
            {
                throw new HttpResponseException(response);
            }

            return list;
        }

        public async Task<PageListCountResult<BreedImages>> GetImagesByBreedIdAsync(string breedId, int page = 0, int limit = 10)
        {
           

            var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/images/search?breed_ids={breedId}&page={page}&limit={limit}&order=ASC");
            var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var list = new PageListCountResult<BreedImages>(page, limit);

            if (response.IsSuccessStatusCode)
            {
                list.PageCount = GetCount(response);

                using var stream = await response.Content.ReadAsStreamAsync();
                var items = stream.ReadAndDeserializeFromJson<List<BreedImagesResult>>();

                foreach (var item in items)
                {
                    list.Results.Add(new BreedImages
                    {
                        Id = item.Id,
                        Width = item.Width,
                        Height = item.Height,
                        Url = item.Url,
                    });
                }
            }
            else
            {
                throw new HttpResponseException(response);
            }
            return list;
        }

        public async Task<PageListCountResult<Images>> GetImagesAsync(int page = 0, int limit = 10)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/images/search?page={page}&limit={limit}");
            var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var list = new PageListCountResult<Images>(page, limit);

            if (response.IsSuccessStatusCode)
            {
                list.PageCount = GetCount(response);

                using var stream = await response.Content.ReadAsStreamAsync();
                var items = stream.ReadAndDeserializeFromJson<List<ImagesResult>>();

                foreach (var item in items)
                {
                    list.Results.Add(new Images
                    {
                            Id = item.Id,
                            Width = item.Width,
                            Height = item.Height,
                            Url = item.Url,
                    });
                }
            }
            else
            {
                throw new HttpResponseException(response);
            }

            return list;
        }

        public async Task<Image> GetImageByIdAsync(string imageId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/images/{imageId}");
            var response = await Client.SendAsync(request,HttpCompletionOption.ResponseHeadersRead);

            var image = new Image();
            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync();
                var item = stream.ReadAndDeserializeFromJson<ImageResult>();

                image.Id = item.Id;
                image.Width = item.Width;
                image.Height = item.Height;
                image.Url = item.Url;
            }
            else
            {
                throw new HttpResponseException(response);
            }
            
            return image;
        }

        private int GetCount(HttpResponseMessage response)
        {
            int count = 0;
            HttpHeaders headers = response.Headers;
            IEnumerable<string> values;
            if (headers.TryGetValues("pagination-count", out values))
            {
                var value = values.First();
                if (int.TryParse(value, out int result))
                {
                    count = result;
                }
            }

            return count;
        }
    }
}
