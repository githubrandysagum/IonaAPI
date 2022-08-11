using IonaAPI.Dtos;
using IonaAPI.IntegrationTest.MockServices;
using IonaAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IonaAPI.IntegrationTest
{
    public class ImageTest : IntegrationTest
    {
        
       
        [Fact]
        public async Task Should_Return_Requested_Image()
        {
            var appService = new AppService(new CatServiceMock(), new DogServiceMock());
            var serviceResult = await appService.GetImageByIdAsync("CatImage1");

            var response = await client.GetAsync(CreateImageByIdRoute("CatImage1"));
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ImageDto>(content);

            Assert.Equal("CatImage1", results?.Id);
        }


        [Fact]
        public async Task Should_Return_Requested_Image_When_Image_Exist_In_Dog()
        {
            var response = await client.GetAsync(CreateImageByIdRoute("DogImage1"));
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ImageDto>(content);

            Assert.Equal("DogImage1", results?.Id);
        }

        [Fact]
        public async Task Should_Return_Emty_When_Not_Found()
        {

            var response = await client.GetAsync(CreateImageByIdRoute("xyz"));
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ImageDto>(content);

            Assert.Null(results);
        }

        [Fact]
        public async Task Should_Return_NotFound202_When_Not_Found()
        {
            var response = await client.GetAsync(CreateImageByIdRoute("xyz"));
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
