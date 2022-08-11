using IonaAPI.Core.ApiResult;
using IonaAPI.Core.Interfaces;
using IonaAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IonaAPI.IntegrationTest.MockServices
{
    internal class CatServiceMock : ICatService
    {
        private List<Breed> breeds = new List<Breed>();
        private List<BreedImages> breedImages = new List<BreedImages>();
        private List<Images> images = new List<Images>();
        private List<Image> singleImages = new List<Image>();

        private string NAME = "Cat";
        public CatServiceMock()
        {
            InitializeAllList();
        }

        private void InitializeAllList()
        {
            for(var i = 0; i < 100; i++)
            {
                var id = i + 1;
                breeds.Add(new Breed
                {
                    Id = $"{NAME}Breed{id}",
                    Name = $"{NAME}Breed{id}",
                    Image = new BreedImage
                    {
                        Id = $"{NAME}BreedImage{id}",
                        Url = $"integrationtest.com/{NAME}Image{id}.jpg"
                    }
                });

                images.Add(new Images
                {
                    Id = $"{NAME}BreedImage{id}",
                    Url = $"integrationtest.com/{NAME}Image{id}.jpg"
                });

                breedImages.Add(new BreedImages
                {
                    Id = $"{NAME}BreedImage{id}",
                    Width = 15+id,
                    Height = 10+id,
                    Url = $"integrationtest.com/{NAME}Image{id}.jpg"
                });

                singleImages.Add(new Image
                {
                    Id = $"{NAME}BreedImage{id}",
                    Width = 15 + id,
                    Height = 10 + id,
                    Url = $"integrationtest.com/{NAME}Image{id}.jpg"
                });
            }
        }

        public async Task<PageListCountResult<Breed>> GetBreedsAsync(int page = 0, int limit = 10)
        {
            var list = breeds.OrderBy(i => i.Id).Skip(page * limit).Take(limit).ToList();

            return await Task.FromResult(new PageListCountResult<Breed>
            {
                PageCount = breeds.Count,
                Page = page,
                Limit = limit,
                Results = list
            });
        }

        public async Task<Image> GetImageByIdAsync(string imageId)
        {
            return await Task.FromResult(singleImages.FirstOrDefault(i => i.Id == imageId));

        }

        public async Task<PageListCountResult<Images>> GetImagesAsync(int page = 0, int limit = 10)
        {
            var list = images.OrderBy(i => i.Id).Skip(page * limit).Take(limit).ToList();

            return await Task.FromResult(new PageListCountResult<Images>
            {
                PageCount = images.Count,
                Page = page,
                Limit = limit,
                Results = list
            });
        }

        public async Task<PageListCountResult<BreedImages>> GetImagesByBreedIdAsync(string breedId, int page = 0, int limit = 10)
        {
            var list = breedImages.OrderBy(i => i.Id).Skip(page * limit).Take(limit).ToList();

            return await Task.FromResult(new PageListCountResult<BreedImages>
            {
                PageCount = images.Count,
                Page = page,
                Limit = limit,
                Results = list
            });
        }
    }
}
