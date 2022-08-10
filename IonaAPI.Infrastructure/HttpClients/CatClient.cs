namespace IonaAPI.Infrastructure.HttpClients
{
    public class CatClient
    {
        public HttpClient Client { get; set; }
        public CatClient(HttpClient client)
        {
            Client = client;
        }
    }
}
