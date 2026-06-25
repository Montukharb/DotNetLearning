using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.State_Configuration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;

namespace EmptyProjectTesting.Background_worker.Flag_State_Worker
{
    public class FlagStateWorker : BackgroundService
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        private readonly State _state;
        private readonly ILogger<FlagStateWorker> _logger;
        public FlagStateWorker(IDbContextFactory<AppDbContext> factory, State state, ILogger<FlagStateWorker> logger)
        {
            _factory = factory;
            _state = state;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                try
                {
                    if (_state.IsEnabled) //state enabled start actually work in background 
                    {
                        using var db = await _factory.CreateDbContextAsync(); //auto close
                        var studentsList = await db.Students.ToListAsync();
                        _logger.LogInformation("Background flag-state-worker start");

                    }
                    else
                    {

                        _logger.LogInformation("flag state idle");
                    }
                    //application stoped wait 5 sec stop worker or cancellation token tigger stop background worker services
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Error inside ");
                }
                try
                {
                    // Isko alag se try-catch me rakha hai taaki jab app stop ho toh OperationCanceledException cleanly catch ho sake.
                    await Task.Delay(5000, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("flag-state-worker background service stopped successfully via cancellation token.");
                    break; // Loop se bahar nikalne ke liye
                }
            }
        }
    }
}
