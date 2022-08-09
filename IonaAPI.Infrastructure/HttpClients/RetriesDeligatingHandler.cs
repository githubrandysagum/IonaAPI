using Polly;
using Polly.Retry;


namespace IonaAPI.HttpClients
{
    public class RetriesDeligatingHandler : DelegatingHandler
    {
        private AsyncRetryPolicy<HttpResponseMessage> _httpRequestRetryPolicy { get; set; }
        public RetriesDeligatingHandler()
        {
            var maxRetryAttempts = 2;
            var pauseBetweenFailures = TimeSpan.FromSeconds(2);

            _httpRequestRetryPolicy = Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await _httpRequestRetryPolicy.ExecuteAsync(async () =>
            {
                return await base.SendAsync(request, cancellationToken);
            });

        }
    }
}
