using Microsoft.AspNetCore.Http.HttpResults;

namespace EmptyProjectTesting.CANCELATION_TOKEN
{
    public class CancelationTokenTesting
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CancelationTokenTesting> _logger;

        public CancelationTokenTesting(HttpClient httpClient, ILogger<CancelationTokenTesting> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<object> ThirdPartyService(CancellationToken cancelToken)
        {
            //var posts = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts", cancelToken).WaitAsync(TimeSpan.FromSeconds(8), cancelToken); problem in this code "Double Cancellation Token"
            try
            {

                using var timeoutcts = new CancellationTokenSource();
                timeoutcts.CancelAfter(TimeSpan.FromSeconds(8));

                using var linkedcts = CancellationTokenSource.CreateLinkedTokenSource(cancelToken, timeoutcts.Token);

                var posts = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts", linkedcts.Token);

                posts.EnsureSuccessStatusCode();

                return posts;
            }
            catch (Exception ex)
            {
                _logger.LogError("Third party service failed message {exception_message}", ex.Message);
                return new
                {
                    Message = "Exception occred during third party service",
                    statusCode = StatusCodes.Status404NotFound
                };
            }
        }
    }
}
