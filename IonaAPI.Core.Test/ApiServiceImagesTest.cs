using IonaAPI.Core.ApiResult;
using IonaAPI.Core.Interfaces;
using IonaAPI.Core.Models;
using IonaAPI.Services;
using Moq;

namespace IonaAPI.Core.Test
{
    public class ApiServiceImagesTest
    {
        private List<Images> images
        {
            get 
            {
                return new List<Images>()
                {
                    new Images()
                    {
                        Id = "1"
                    },
                    new Images()
                    {
                        Id = "2"
                    },
                    new Images()
                    {
                        Id = "3"
                    },
                    new Images()
                    {
                        Id = "4"
                    }
                };
            }
        }

        [Fact]
        public async Task ReturnListItemsWhenClientHasDataAsync()
        {
            var pageList = new PageListCountResult<Images>(0, 0, images);

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p =>  p.GetImagesAsync(0,10)).Returns(Task.FromResult(pageList));
            dogService.Setup(p => p.GetImagesAsync(0, 10)).Returns(Task.FromResult(new PageListCountResult<Images>()));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetImagesAsync(0, 10);

            Assert.Equal(4, result.Results.Count);
        }

        [Fact]
        public async Task ReturnCatOnlyWhenCatApiHasAllDataAsync()
        {
            var catList = new PageListCountResult<Images>(0, 1, images);
            var dogList = new PageListCountResult<Images>(0, 1, new List<Images>
            {
                new Images
                {
                    Id = "dog"
                }
            });

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p => p.GetImagesAsync( 0, 3)).Returns(Task.FromResult(catList));
            dogService.Setup(p => p.GetImagesAsync(0, 3)).Returns(Task.FromResult(dogList));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetImagesAsync(0, 3);

            Assert.DoesNotContain(result.Results, i => i.Id == "dog");
        }

        [Fact]
        public async Task ReturnsDogDataWhenCatHasNoData()
        {
            var catList = new PageListCountResult<Images>(0, 1, new List<Images>());
            var dogList = new PageListCountResult<Images>(0, 1, new List<Images>
            {
                new Images
                {
                    Id = "dog"
                },
                new Images
                {
                    Id = "dog1"
                }
            });

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p => p.GetImagesAsync(0, 3)).Returns(Task.FromResult(catList));
            dogService.Setup(p => p.GetImagesAsync(0, 3)).Returns(Task.FromResult(dogList));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetImagesAsync(0, 3);

            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task ReturnsCompleteDataWhenCatHasNoEnoughData()
        {
            var catList = new PageListCountResult<Images>(0, 10, images);
            var dogList = new PageListCountResult<Images>(0, 10, new List<Images>
            {
                new Images
                {
                    Id = "dog"
                },
                new Images
                {
                    Id = "dog1"
                }
            });

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p => p.GetImagesAsync(0, 10)).Returns(Task.FromResult(catList));
            dogService.Setup(p => p.GetImagesAsync(0, 10)).Returns(Task.FromResult(dogList));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetImagesAsync(0, 10);

            Assert.Equal(6, result.Results.Count);
        }
    }
}