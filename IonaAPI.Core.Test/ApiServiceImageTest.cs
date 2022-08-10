using IonaAPI.Core.ApiResult;
using IonaAPI.Core.Interfaces;
using IonaAPI.Core.Models;
using IonaAPI.Services;
using Moq;

namespace IonaAPI.Core.Test
{
    public class ApiServiceImageTest
    {
        [Fact]
        public async Task Return_Image_From_CatApi_If_It_Has_the_Image()
        {
          
            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            catService.Setup(p =>  p.GetImageByIdAsync("a")).Returns(Task.FromResult(new Image { Id = "x"}));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetImageByIdAsync("a");

            Assert.Equal("x", result.Id);
        }

        [Fact]
        public async Task Return_Image_From_DogApi_If_CatAPI_Dont_Have_The_Image()
        {

            var catService = new Mock<ICatService>();
            var dogService = new Mock<IDogService>();
            dogService.Setup(p => p.GetImageByIdAsync("a")).Returns(Task.FromResult(new Image { Id = "x" }));

            var appService = new AppService(catService.Object, dogService.Object);

            var result = await appService.GetImageByIdAsync("a");

            Assert.Equal("x", result.Id);
        }


    }
}