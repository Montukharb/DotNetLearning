using EmptyProjectTesting.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EmptyProjectTesting.Background_worker
{
    public class Worker : BackgroundService
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        private readonly ILogger<Worker> _logger;
        public Worker(IDbContextFactory<AppDbContext> factory, ILogger<Worker> logger)
        {
            _factory = factory;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Har baar loop chalne par naya DbContext instance create hoga 
            // aur block khatam hote hi automatically dispose ho jayega
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var db = await _factory.CreateDbContextAsync();

                    var students = await db.Students.ToListAsync();

                    // Any operation as we need to do here

                    // ERROR FIXED: Yahan 'stoppingToken' pass karna zaroori hai.
                    // Isse agar application stop hogi, toh 5 second ka wait turant cancel ho jayega.
                    await Task.Delay(5000, stoppingToken);
                }
                catch (OperationCanceledException) //run when app close
                {
                    _logger.LogInformation("Worker background service stoped successfully");
                     
                    break; //graceful break
                }
            }
        }
    }  //Note: isme problem aye gi long term ma jab heavy task ho jaise intance create dispose multiple time solution hoga AddPooledDbContextFactory
}
/*
Singleton Object: Hamesha ek hi rahega (Jab tak app chal rahi hai).

CancellationToken: Har HTTP request ke sath naya banta hai aur request khatam hote hi expire ho jata hai.

Rule: Singleton ke andar kabhi bhi CancellationToken ko class-level variable ya property bana kar store mat karo. Hamesha use Method ke arguments (parameters) ke zariye pass karo. 
 */