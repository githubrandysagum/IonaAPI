namespace IonaAPI.Infrastructure.HttpClients
{
    public class DogClient
    {
        public HttpClient Client { get; set; }
        public DogClient(HttpClient client)
        {
            Client = client;
        }
    }
}
