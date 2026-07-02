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
                    using var db = await _factory.CreateDbContextAsync(stoppingToken);

                    var students = await db.Students.ToListAsync(stoppingToken);

                    // Any operation as we need to do here

                    // ERROR FIXED: Yahan 'stoppingToken' pass karna zaroori hai.
                    // Isse agar application stop hogi, toh 5 second ka wait turant cancel ho jayega.
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception occur {exception-message}", ex.Message);
                }
                try
                {
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

/*
 
1. Scheduled Tasks & Cron Jobs (Time-based Tasks)
    • Database Cleanup: Purane logs, expired sessions, ya temporary data ko daily delete karna.
    • Daily Reports: Har raat ko business metrics calculate karke admin ko report bhejna.
    • Subscription Check: Expired premium users ka status automatically update karna.
2. Queue Processing & Message Consumption
    • RabbitMQ / Kafka Consumer: Message brokers se messages ko receive karna aur background me process karna.
    • Email Queue: Jab koi user register kare, toh email bhejte waqt main request ko block na karke message queue me dalna, fir background service se use send karna.
    • Order Processing: E-commerce me order place hone ke baad payment confirmation aur inventory update background me handle karna.
3. Data Syncing & Polling
    • Third-Party API Polling: Har 15 minute me kisi external API (jaise stock prices ya weather data) se data fetch karke local database me save karna.
    • Cache Refreshing: Heavy database queries ka data periodically refresh karke Redis cache me update karte rehna.
4. Real-time Monitoring & Alerts
    • System Health Check: Server ki health, memory usage, ya server availability ko continuous monitor karna.
    • Alert Notifications: Agar system me koi critical error aaye toh developers ko Slack ya Microsoft Teams par turant alert bhejna.
5. File Processing
    • Bulk Uploads: User ki upload ki hui badi Excel, CSV, ya image files ko chunk-by-chunk read aur process karna taaki web application slow na ho.
 */