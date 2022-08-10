using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IonaAPI.IntegrationTest
{
    public class IntegrationTest
    {
        [Fact]
        public async Task Should_Response_With_Default_Route_When_Not_Specify()
        {
            var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
               
                    
            });

            var client = application.CreateClient();

            var response =  await client.GetAsync("/Breeds?page=0&limit10");

        }
    }
}
