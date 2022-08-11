using IonaAPI.Core.Interfaces;
using IonaAPI.IntegrationTest.MockServices;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IonaAPI.IntegrationTest
{
    public class IntegrationTest
    {
        protected HttpClient client;
        
        public IntegrationTest()
        {
            var application = new WebApplicationFactory<Program>().
                WithWebHostBuilder(builder =>
                {
                    // ... Configure test services
                    builder.ConfigureServices(services => {
                        services.Remove(services.Single(s => s.ServiceType == typeof(ICatService)));  // remove service
                        services.Remove(services.Single(s => s.ServiceType == typeof(IDogService)));  // remove service
                        services.AddSingleton<ICatService, CatServiceMock>();
                        services.AddSingleton<IDogService, DogServiceMock>();                    // add mock
                    });
                });



            client = application.CreateClient();
        }

        protected string CreateBreedsRoute(int page, int limit)
        {
            return $"api/v1/breeds?page={page}&limit={limit}";
        }

        protected string CreateImagesByBreedRoute(string breedId, int page, int limit)
        {
            return $"api/v1/Breeds/{breedId}?page={page}&limit={limit}";
        }

        protected string CreateImagesRoute(int page, int limit)
        {
            return $"api/v1/images?page={page}&limit={limit}";
        }

        protected string CreateImageByIdRoute(string id)
        {
            return $"api/v1/images/{id}";
        }
    }
}
