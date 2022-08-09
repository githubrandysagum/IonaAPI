using IonaAPI.Core.Interfaces;
using IonaAPI.Infrastructure.HttpClients;

namespace IonaAPI.Infrastructure
{
    public class CatService : BaseService, ICatService
    {
        public CatService(CatClient client) : base(client.Client)
        {
        }
    }
}
