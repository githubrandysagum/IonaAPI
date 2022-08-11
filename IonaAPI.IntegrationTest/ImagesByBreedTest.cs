using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IonaAPI.IntegrationTest
{
    public class ImagesByBreedTest : IntegrationTest
    {
        [Fact]
        public async Task Return_BadRequest_When_Page_Is_LessThan_Zero()
        {
            var response = await client.GetAsync(CreateImagesByBreedRoute("xyz",-1, 1));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async Task Return_BadRequest_When_Limit_Is_Out_Of_Range()
        {
            //Limit less than 1
            var response = await client.GetAsync(CreateImagesByBreedRoute("xyz", 0, 0));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            //Limit greater than 100
            var response2 = await client.GetAsync(CreateImagesByBreedRoute("xyz", 1, 101));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
