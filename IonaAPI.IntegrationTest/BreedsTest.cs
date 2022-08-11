using IonaAPI.Core.ApiResult;
using IonaAPI.Core.Models;
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
    public class BreedsTest : IntegrationTest
    {
        [Fact]
        public async Task Return_BadRequest_When_Page_Is_LessThan_Zero()
        {
            var response = await client.GetAsync(CreateBreedsRoute(-1, 1));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async Task Return_BadRequest_When_Limit_Is_Out_Of_Range()
        {
            //Limit less than 1
            var response = await client.GetAsync(CreateBreedsRoute(0, 0));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            //Limit greater than 100
            var response2 = await client.GetAsync(CreateBreedsRoute(1, 101));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Items_Count_Equal_To_Limit()
        {
            var response = await client.GetAsync(CreateBreedsRoute(0, 10));
            var content = await response.Content.ReadAsStringAsync();
            var results =  JsonConvert.DeserializeObject<PageListResult<BreedDto>>(content);

            Assert.NotNull(results);
            Assert.Equal(10, results?.Results.Count);
        }

        [Fact]
        public async Task Should_Return_Correct_Page()
        {
            var appService = new AppService(new CatServiceMock(), new DogServiceMock());
            var serviceResult = await appService.GetBreedsAsync(1, 80);

            var cats = serviceResult.Results.Where(b => b.Id.Contains("Cat"));
            var dogs = serviceResult.Results.Where(b => b.Id.Contains("Dog")).ToList();

            Assert.Equal(20, cats.Count());
            Assert.Equal(60, dogs.Count());


            var response = await client.GetAsync(CreateBreedsRoute(1, 80));
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<PageListResult<BreedDto>>(content);

            var catResult = results.Results.Where(b => b.Id.Contains("Cat"));
            var dogResult = results.Results.Where(b => b.Id.Contains("Dog"));
            
            
            Assert.Equal(20, catResult.Count());
            Assert.Equal(60, dogResult.Count());
        }
    }
}
