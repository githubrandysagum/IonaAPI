using IonaAPI.Core.ApiResult;
using IonaAPI.Core.Interfaces;
using IonaAPI.Core.Models;
using IonaAPI.Services;
using Moq;

namespace IonaAPI.Core.Test
{
    public class ApiServiceImageByBreedTest
    {
        private List<BreedImages> images
        {
            get 
            {
                return new List<BreedImages>()
                {
                    new BreedImages()
                    {
                        Id = "1"
                    },
                    new BreedImages()
                    {
                        Id = "2"
                    },
                    new BreedImages()
                    {
                        Id = "3"
                    },
                    new BreedImages()
                    {
                        Id = "4"
                    }
                };
            }
        }

        [Fact]
        public async Task Returns_ListItems_When_Client_Has_DataAsync()
        {
            var pageList = new PageListCountResult<BreedImages>(0, 0, images);

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p =>  p.GetImagesByBreedIdAsync("a",0,10)).Returns(Task.FromResult(pageList));
            dogService.Setup(p => p.GetImagesByBreedIdAsync("a", 0, 10)).Returns(Task.FromResult(new PageListCountResult<BreedImages>()));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetImagesByBreedIdAsync("a", 0, 10);

            Assert.Equal(4, result.Results.Count);
        }

        [Fact]
        public async Task Returns_Cat_Data_OnlyWhen_CatApi_Has_All_DataAsync()
        {
            var catList = new PageListCountResult<BreedImages>(0, 1, images);
            var dogList = new PageListCountResult<BreedImages>(0, 1, new List<BreedImages>
            {
                new BreedImages
                {
                    Id = "dog"
                }
            });

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p => p.GetImagesByBreedIdAsync("a", 0, 3)).Returns(Task.FromResult(catList));
            dogService.Setup(p => p.GetImagesByBreedIdAsync("a", 0, 3)).Returns(Task.FromResult(dogList));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetImagesByBreedIdAsync("a",0, 3);

            Assert.DoesNotContain(result.Results, i => i.Id == "dog");

        }

        [Fact]
        public async Task Returns_Dog_Data_When_Cat_Has_No_DataAsync()
        {
            var catList = new PageListCountResult<BreedImages>(0, 1, new List<BreedImages>());
            var dogList = new PageListCountResult<BreedImages>(0, 1, new List<BreedImages>
            {
                new BreedImages
                {
                    Id = "dog"
                },
                new BreedImages
                {
                    Id = "dog1"
                }
            });

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p => p.GetImagesByBreedIdAsync("a", 0, 3)).Returns(Task.FromResult(catList));
            dogService.Setup(p => p.GetImagesByBreedIdAsync("a", 0, 3)).Returns(Task.FromResult(dogList));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetImagesByBreedIdAsync("a", 0, 3);

            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task Returns_Complete_Data_When_Cat_Has_No_Enough_DataAsync()
        {
            var catList = new PageListCountResult<BreedImages>(0, 10, images);
            var dogList = new PageListCountResult<BreedImages>(0, 10, new List<BreedImages>
            {
                new BreedImages
                {
                    Id = "dog"
                },
                new BreedImages
                {
                    Id = "dog1"
                }
            });

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p => p.GetImagesByBreedIdAsync("a", 0, 10)).Returns(Task.FromResult(catList));
            dogService.Setup(p => p.GetImagesByBreedIdAsync("a", 0, 10)).Returns(Task.FromResult(dogList));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetImagesByBreedIdAsync("a", 0, 10);

            Assert.Equal(6, result.Results.Count);
        }
    }
}