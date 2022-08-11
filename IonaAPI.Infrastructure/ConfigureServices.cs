using IonaAPI.Core.Interfaces;
using IonaAPI.HttpClients;
using IonaAPI.Infrastructure.HttpClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IonaAPI.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var catApiKey = configuration.GetSection("CatApiSettings").Get<ApiSettings>();
            var dogApiKey = configuration.GetSection("DogApiSettings").Get<ApiSettings>();

            services.AddScoped<RetriesDeligatingHandler>();
            services.AddHttpClient<CatClient>(client =>
            {
                client.BaseAddress = new Uri(catApiKey.Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("x-api-key", catApiKey.ApiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            }).AddHttpMessageHandler<RetriesDeligatingHandler>();

            services.AddHttpClient<DogClient>(client =>
            {
                client.BaseAddress = new Uri(dogApiKey.Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("x-api-key", dogApiKey.ApiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler<RetriesDeligatingHandler>();

            services.AddSingleton<ICatService, CatService>();
            services.AddSingleton<IDogService, DogService>();
            return services;
        }
    }
}
