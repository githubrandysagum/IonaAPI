using IonaAPI.Core.ApiResult;
using IonaAPI.Core.Interfaces;
using IonaAPI.Core.Models;
using IonaAPI.Services;
using Moq;

namespace IonaAPI.Core.Test
{
    public class ApiServiceBreedsTest
    {
        private List<Breed> breeds
        {
            get 
            {
                return new List<Breed>()
                {
                    new Breed()
                    {
                        Id = "1"
                    },
                    new Breed()
                    {
                        Id = "2"
                    },
                    new Breed()
                    {
                        Id = "3"
                    },
                    new Breed()
                    {
                        Id = "4"
                    }
                };
            }
        }

        [Fact]
        public async Task ReturnListItemsWhenClientHasDataAsync()
        {
            var pageList = new PageListCountResult<Breed>(0, 0, breeds);

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p =>  p.GetBreedsAsync(0,10)).Returns(Task.FromResult(pageList));
            dogService.Setup(p => p.GetBreedsAsync(0, 10)).Returns(Task.FromResult(new PageListCountResult<Breed>()));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetBreedsAsync(0, 10);

            Assert.Equal(4, result.Results.Count);
        }

        [Fact]
        public async Task ReturnCatOnlyWhenCatApiHasAllDataAsync()
        {
            var catList = new PageListCountResult<Breed>(0, 1, breeds);
            var dogList = new PageListCountResult<Breed>(0, 1, new List<Breed>
            {
                new Breed
                {
                    Id = "dog"
                }
            });

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p => p.GetBreedsAsync(0, 3)).Returns(Task.FromResult(catList));
            dogService.Setup(p => p.GetBreedsAsync(0, 3)).Returns(Task.FromResult(dogList));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetBreedsAsync(0, 3);

            Assert.DoesNotContain(result.Results, i => i.Id == "dog");

        }

        [Fact]
        public async Task ReturnsDogDataWhenCatHasNoData()
        {
            var catList = new PageListCountResult<Breed>(0, 1, new List<Breed>());
            var dogList = new PageListCountResult<Breed>(0, 1, new List<Breed>
            {
                new Breed
                {
                    Id = "dog"
                },
                new Breed
                {
                    Id = "dog1"
                }
            });

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p => p.GetBreedsAsync(0, 3)).Returns(Task.FromResult(catList));
            dogService.Setup(p => p.GetBreedsAsync(0, 3)).Returns(Task.FromResult(dogList));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetBreedsAsync(0, 3);

            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task ReturnsCompleteDataWhenCatHasNoEnoughData()
        {

            var catList = new PageListCountResult<Breed>(0, 10, breeds);
            var dogList = new PageListCountResult<Breed>(0, 10, new List<Breed>
            {
                new Breed
                {
                    Id = "dog"
                },
                new Breed
                {
                    Id = "dog1"
                }
            });

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p => p.GetBreedsAsync(0, 10)).Returns(Task.FromResult(catList));
            dogService.Setup(p => p.GetBreedsAsync(0, 10)).Returns(Task.FromResult(dogList));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetBreedsAsync(0, 10);

            Assert.Equal(6, result.Results.Count);
        }
    }
}