using IonaAPI.Core.Interfaces;
using IonaAPI.Infrastructure.HttpClients;
using IonaAPI.Services;

namespace IonaAPI.Infrastructure
{
    public class DogService : BaseService, IDogService
    {
        public DogService(DogClient client) : base(client.Client)
        {
        }
    }
}
