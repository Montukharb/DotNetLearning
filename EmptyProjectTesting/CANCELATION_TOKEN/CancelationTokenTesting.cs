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
        public async Task<HttpResponseMessage> ThirdPartyService(CancellationToken cancellationToken)
        {
            using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(8));

            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken,timeoutCts.Token);

            try
            {
                var response = await _httpClient.GetAsync(
                    "https://jsonplaceholder.typicode.com/posts",
                    linkedCts.Token);

                //Throw and exception if status code is 400,404,500,etc.
                response.EnsureSuccessStatusCode();

                return response;
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Request was cancelled.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Third party service failed.");
                throw;
            }
        }
        public async Task AllOperations(CancellationToken token)
        {
            if(token.IsCancellationRequested) // Returns true if cancellation has been requested.
            {
                _logger.LogInformation("Stopped");
            }
            try
            {
                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Cancelled");
            }
        }
    }
}

