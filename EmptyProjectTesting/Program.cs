using Elastic.CommonSchema;
using Elastic.Serilog.Sinks;
using EmptyProjectTesting.Background_worker;
using EmptyProjectTesting.Background_worker.Flag_State_Worker;
using EmptyProjectTesting.ControllerActionFilter;
using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.Endpoints;
using EmptyProjectTesting.Middleware;
using EmptyProjectTesting.ParallelProgramming;
using EmptyProjectTesting.Race_Condition;
using EmptyProjectTesting.Repository;
using EmptyProjectTesting.Services;
using EmptyProjectTesting.State_Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.FileProviders;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Serilog.Sinks.MSSqlServer;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var dbConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var appName = builder.Configuration["MySetting:AppName"];
//builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(dbConnection, sqlOptions =>
//{
//    sqlOptions.EnableRetryOnFailure(
//        maxRetryCount: 5,
//        maxRetryDelay: TimeSpan.FromSeconds(5),
//        errorNumbersToAdd: null
//        );
//})); //default lifetime dbContext == Scoped
/* Problem with scoped dbcontext
   1 HTTP request 1 DbContext instance auto dispose
   When problem occured in scoped dbcontext ? => Background service, Parallel processing, Task.Run(), blazor service
   Error occur -> cannot consumed scoped service from singleton
   
Solution AppDbContextFactory -> ye dbcontext ko Sigleton Service ma add karti hai or parallel processing
factory = dbcontext ko track nahi karti auto disclose nahi hota mannually handle by using statement
dbcontext factory work => jab bhi context chaiya new context create karti hai har bar new object

# how to use
--- 1. Reqister adddbcontextfactory in program.cs
--- 2. when need context dependency use it 
  { 
   step 1. IDbContextFactory<AppdbContext> factory
    _factory = factory;
  step 2. create dbcontext as a service
        using var db = await _factory.CreateDbContextAsync(); //new Context provided not use it
         
        return await db.students.ToListAsync();
  }
 */


//builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(dbConnection, sqlOptions =>
//{
//    sqlOptions.EnableRetryOnFailure(
//        maxRetryCount: 5,
//        maxRetryDelay: TimeSpan.FromSeconds(5),
//        errorNumbersToAdd: null
//        );
//}));


//DbContext == Singleton Without tracking handle using statement when it is used any action.
/*
Toh.NET Core ka Dependency Injection system background mein do (2) kaam ek sath karta hai:
1. Wo IDbContextFactory<AppDbContext> ko register karta hai (jo aapki background service use kar rahi hai).
2. (Sabse Zaroori) Wo internally normal AppDbContext ko bhi as a Scoped service register kar deta hai.
*/
//builder.Services.AddDbContext<>(); AddDbContext or AddDbContextPool eak sath registern nahi karte kyuki dono hi appDbcontext ko register karte hai eak hi sufficient hai.
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(dbConnection, sqlOptions =>
{
    sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(5),
        errorNumbersToAdd: null

        ); //pooled use hoga normal addDbcontext jaise efficient works karne me 

}), poolSize: 2048); //by default poolsize 1024

builder.Services.AddPooledDbContextFactory<AppDbContext>(options => options.UseSqlServer(dbConnection, sqlOptions =>
{
    sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(5),
        errorNumbersToAdd: null
        );

}));


//PooledDbContextFactory use hevay traffic and high performance ka liya use hota hai ya internally 20-30 instance create kar ke rakhta hai user ki need hone par whi se object pick karta hai service complete hone ka bad wapis pool me store aise karne se bar bar creationg dispose ki problem khatam ho zati hai
// ============================================================================
// ⚠️ POOLED DBCONTEXT FACTORY RULES (IMPORTANT FOR PRODUCTION)
// ============================================================================

// RULE 1: STRICT STATELESSNESS (Sabse bada rule)
// ----------------------------------------------------------------------------
// DbContext class ke andar koi bhi custom runtime variables ya state store mat karna.
// Example: public int CurrentUserId { get; set; } // ❌ DO NOT DO THIS!
// Kyuki jab ek instance reuse hoga, toh User-1 ka data User-2 ko leak ho jayega.
// DbContext must be 100% clean and stateless.


// RULE 2: ALWAYS USE THE 'USING' STATEMENT (Dispose is mandatory)
// ----------------------------------------------------------------------------
// 'using var db = await _factory.CreateDbContextAsync();' likhna bilkul mat bhulna.
// Normal factory me 'using' na lagane se Memory Leak hota hai.
// Lekin Pooled factory me agar 'using' nahi lagaya, toh context kabhi Pool me wapas nahi jayega.
// Natija? Pool jaldi hi khali (exhaust) ho jayega aur app hang ho jayegi.


// RULE 3: DO NOT BENCHMARK / CONFIGURE INSIDE 'ONCONFIGURING'
// ----------------------------------------------------------------------------
// DbContext ki 'OnConfiguring' method ke andar heavy or dynamic configuration mat likho.
// Pooled factory me 'OnConfiguring' sirf ek baar chalti hai jab pool pehli baar banta hai.
// Har baar 'CreateDbContextAsync' call karne par ye method dubara RUN NAHI HOTI.


// RULE 4: MULTI-THREADING AND THREAD-SAFETY
// ----------------------------------------------------------------------------
// Ek 'DbContext' instance ek waqt me sirf EK HI THREAD par chal sakta hai.
// Agar 'Parallel.ForEach' ya 'Task.WhenAll' use kar rahe ho, toh har loop/thread ke andar 
// 'await _factory.CreateDbContextAsync()' karke alag-alag instance nikalna mandatory hai.
// Ek hi instance ko multiple parallel tasks me share mat karna.


// RULE 5: DEFAULT POOL SIZE CAUTION
// ----------------------------------------------------------------------------
// By default, .NET pool ka size 1024 rakhta hai. 
// Agar aapko lagta hai ki heavy load me 1024 se zyada concurrent connections lagenge,
// toh Program.cs me 'poolSize: 2000' manually configure kar lena.
// Example: builder.Services.AddPooledDbContextFactory<AppDbContext>(opt => ..., poolSize: 2000);
// ============================================================================

//background class register
builder.Services.AddHostedService<Worker>(); //worker class auto managed and executed when app run and stop when app closed

//for use custom in memory flag state handler register as singleton
builder.Services.AddSingleton<State>();
builder.Services.AddHostedService<FlagStateWorker>(); //work as normal worker but better control user can now process or idle in hand button.

//First always prefer to register services and repository then controller
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentServices, StudentService>();

//parallelprogramming service register
builder.Services.AddScoped<ParallelProgram>();

//RaceConditonServices register
builder.Services.AddScoped<RaceProgram>();

builder.Services.AddHealthChecks();

var SinkOptions = new MSSqlServerSinkOptions
{
    TableName = "Logs",
    AutoCreateSqlTable = true
};


builder.Host.UseSerilog((context, config) =>
{

    if (context.HostingEnvironment.IsDevelopment())
    {
        config.WriteTo.Console();
        config.WriteTo.File("Logs/log.txt", shared: false, rollingInterval: RollingInterval.Day);
        config.WriteTo.File(new JsonFormatter(), "Json_Logs/log.json", rollingInterval: RollingInterval.Day);
        config.WriteTo.MSSqlServer(dbConnection, sinkOptions: SinkOptions); //just checking
    }
    else
    {
        //config.WriteTo.File("Logs/log.txt",shared:false,rollingInterval:RollingInterval.Day);
        //config.WriteTo.File(new JsonFormatter(),"Logs_Json/log.json"); //json format
        config.WriteTo.File(new CompactJsonFormatter(),
            "Compact_Logs_Json/log.json",
            retainedFileCountLimit: 30 //last 30 file rakho baki auto 
            ); //json format with compact size industry type use 

        //----------Database Save log ------------

        //config.WriteTo.MSSqlServer(dbConnection, sinkOptions: SinkOptions); //data base ma logs save hogi
    }
});
/*
 Industry Me Common Setting
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.File(
        new Serilog.Formatting.Json.JsonFormatter(),
        "logs/log-.json",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        fileSizeLimitBytes: 10485760,
        rollOnFileSizeLimit: true);
});

Isme:

Daily file
JSON format
10 MB par new file
Last 30 days ke logs preserve
Usse purane logs auto delete
Summary
Setting	Kaam
rollingInterval: Day	Roz nayi file
retainedFileCountLimit: 7	Last 7 files rakho
fileSizeLimitBytes	Max file size
rollOnFileSizeLimit: true	Size limit cross hote hi nayi file
JsonFormatter()	JSON logs
Default behavior	Auto delete nahi hoti
 */


//Log.Logger = new LoggerConfiguration().WriteTo.File("").CreateLogger();


//😡-------------- Serilog json formating code ---------------


/*
 Option 1: JsonFormatter
using Serilog.Formatting.Json;

builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.File(
        new JsonFormatter(),
        "logs/log.json");
});

Output:

{
  "Timestamp":"2026-06-22T10:20:15",
  "Level":"Information",
  "MessageTemplate":"User {UserId} Login"
}
Option 2: Compact JSON (Industry me kaafi popular)

Package:

dotnet add package Serilog.Formatting.Compact

Code:

using Serilog.Formatting.Compact;

builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.File(
        new CompactJsonFormatter(),
        "logs/log.json");
});

Output:
{"@t":"2026-06-22T10:20:15","@l":"Information","@mt":"User {UserId} Login","UserId":1}


Ye file size kam rakhta hai.
 */


//----------Database base complete example -------------

/*
 using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

var sinkOptions = new MSSqlServerSinkOptions
{
    TableName = "Logs",
    AutoCreateSqlTable = true
};

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: sinkOptions)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
 */

//--------------- Elastic Search -----------------
/*
 isme file ka name or path automatic set hota hai 
 logs index basis par store hoti hai 
 */

builder.Services
       .AddControllers(options => options.Filters.Add<DepartmentActionFilter>()) //departmentActionFilter global 
       .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.ReferenceHandler =
               ReferenceHandler.IgnoreCycles;
       }
       ); //include se cycle banti hai unko remove karta hai kai bar error bhi de sakta hai better hoga dto use kare
builder.Services.AddScoped<FlagActionFilter>();//ServiceFilter ma Registration karna padta hai typeFilter ma need nahi hai

//department action filter ab global level par add ho gaya hai filter complete app par apply hoga ab

//builder.Services.AddControllers(options => { options.Filters.Add<DepartmentActionFilter>(); });

//        context.Response.ContentType = "application/json";

//builder.Services.AddControllers(); //controller register karta hai all

var app = builder.Build();
app.Use((context, next) =>
{
    //context.Response.WriteAsync("Use middleware First");
    Console.WriteLine("use middleware first");
    return next();

}
);
app.Use(next => //use yahi karna chaiya asp.net first preference {Simplicity ka liye wrapper wala bhi use kar sakte hai}
{
    return async context =>
    {
        // Log the incoming request details
        Console.WriteLine($"Incoming Request: {context.Request.Method} {context.Request.Path}");

        // Call the next middleware in the pipeline
        await next(context);
        // Log the outgoing response details
        Console.WriteLine($"Outgoing Response: {context.Response.StatusCode}");
    };
});
//global level exception handler 3 ways
//app.UseExceptionHandler(); // 1. this is inbuild middleware we can overrite this middleware
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
//app.UseExceptionHandler("/error");2. global exception handler using controller koi bhi eak use kar sakte hai recommended hai custom class wala
//app.UseExceptionHandler("/error");3. ye map ka sath bhi use kar sakte hai 
//app.Map("/error", branch =>
//{
//    branch.Run(async context =>
//    {
//        context.Response.StatusCode = 500;
//        context.Response.ContentType = "application/json";
//        await context.Response.WriteAsJsonAsync(
//            new
//            {
//                message = "Something went wrong",
//            }
//            );

//branch.Run(async context =>
//    {
//        var exceptionfeature = context.Features.Get<IExceptionHandlerFeature>();
//        context.Response.StatusCode = 500;
//        await context.Response.WriteAsJsonAsync(new
//        {
//            message = "Something went wrodsfng",
//            exceptionProperMsg = exceptionfeature?.Error?.Message,
//            errorStack = exceptionfeature?.Error?.StackTrace
//        });
//    });
//});

// 4.  labda option with inbuild
//app.UseExceptionHandler(errorApp =>
//{
//    errorApp.Run(async context =>
//    {
//        context.Response.StatusCode = 500;
//        context.Response.ContentType = "application/json";
//        await context.Response.WriteAsJsonAsync( new { 
//              message = "Something went wrong"
//        });
//    });
//});
app.UseStatusCodePages(); // generate custom text or structured responses for common HTTP error status codes (400–599) that otherwise return an empty response body
                          //Agar ise use nahi karte aur application me 404 error aata hai, toh user ko browser me bilkul blank page dikhega. Yeh middleware us blank response ko intercept karke meaningful error message dikhata hai.

// Custom Lambda HandlerAgar aapko logic lagana ho (jaise 401 aane par login page par redirect karna):
/* 

 usestatuscodepages lamda handler ka use tabhi karte hai jab har response error par same jagah data or status code send karna ho

app.UseStatusCodePages(async context =>
   {
       var response = context.HttpContext.Response;

       if (response.StatusCode == 401)
       {
           // Batao ki hum JSON bhej rahe hain
           response.ContentType = "application/json";

           // Custom error message create karo
           var errorObject = new { message = "Aap logged in nahi hain. Kripya login karein." };

           // JSON string me convert karke send karo
           await response.WriteAsJsonAsync(errorObject);
       }

   });

*/
app.UseHttpsRedirection(); //http request convert/forworded into https
app.UseStaticFiles();

/*use static file me overload bhi hota hai wwwroot ma other path se data bhejna ho example*/
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "PrivateAssets")),
    RequestPath = "/ssetas" /*requestpath me route kuch aise bane ga Ab user URL me "/assets/image.png" likh kar file access kar payega. */
}); /*
wwwroot files me stored  static files (jaise HTML, CSS, JavaScript, Images, Videos, aur Fonts) directly browser ko serve karne ki permission deta hai.
According to security[
1. Agar aapne sirf app.UseStaticFiles() likha hai, toh wwwroot folder ke andar ki koi bhi file bilkul public hoti hai. Agar kisi user ko file ka exact path (URL) pata hai, toh woh bina kisi login ya authorization ke use browser me open ya download kar sakta hai.
2. Yeh security ke hisab se ek bada risk ho sakta hai agar aap wahan sensitive files rakh rahe hain.
]  
Note: Isse bachne ka 2 main ways hai 

way-1. Jo files private hain (jaise users ke Aadhar Card, Invoices, ya PDFs), unhe wwwroot me mat rakhein. Unhe project ke bahar kisi alag folder me rakhlein aur ek Authorized Controller Action ke zariye serve karein.
 [Authorize] // Sirf logged-in users hi access kar payenge
---c-o-d-e---------------
[HttpGet("download/{fileName}")]
public IActionResult DownloadFile(string fileName)
{
    // wwwroot ke bahar ka path
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "PrivateFiles", fileName); 
    
    if (!System.IO.File.Exists(filePath))
        return NotFound();

    var fileBytes = System.IO.File.ReadAllBytes(filePath);
    return File(fileBytes, "application/pdf", fileName);
}
------e-n-d-------

                       
                       */
//custom class middleware
app.UseMiddleware<CustomMiddleware>();
app.UseMiddleware<CustomMiddleware2>();
//specific route middleware
app.Map("/admin", branch =>
{
    branch.Use(async (context, next) =>
    {
        Console.WriteLine($"Method = {context.Request.Method}\npath = {context.Request.PathBase}\nActiveIpAddress = {context.Connection.RemoteIpAddress}");

        await next();
    });

    branch.Use(async (context, next) =>
    {
        if (context.Request.Path.StartsWithSegments("/danzer"))
        {
            await context.Response.WriteAsync("Stopped pipeline");
            return; //middleware response end here

        }
        await next();
    });
});

app.UseWhen(
    context => context.Request.Path.StartsWithSegments("/tree"),
    branch =>
    {
        branch.Use(async (context, next) =>
        {
            Console.WriteLine(context.Request.Path);
            await next();
        });
        //branch.Use(async (a, b) => { await b(); });
    }

    );

app.MapControllers(); //ye route ko map karta hai controller ke action method ke sath jese ki http get post put delete etc. routes ko match karta hai sabhi controller ke app.useRouting automatic laga deta hai net 8+ version me
app.MapGet("/", () => "Welcome to asp.net core web api " + appName); //minimal api example
//wild card routes handled by inbuild minimal api routing feature ye internall routing system me register hota hai hamesha last ma place hoga iss se phele minimal api use kar sakte hai 
//app.Map("/{*path}", branch => { }); // ye bhi unknown route handle kar sakta hai rarely use hota hai
app.MapCountryFlagEndpoints(); //endpoints register /call normally kha sakte hai minimal api call ho rahi ha
app.MapTestEndpoint();
app.MapHealthChecks("/health");
app.MapFallback(() =>
{
    return Results.NotFound("EndPoint Route Not Found");
});
app.Run();



// nested map
//app.Map("/admin", admin =>
//{
//    admin.Use(async (context, next) =>
//    {
//        Console.WriteLine("Admin Branch");
//        Console.WriteLine(context.Request.PathBase);
//        Console.WriteLine(context.Request.Path);

//        await next();
//    });

//    admin.Map("/student", student =>
//    {
//        student.Use(async (context, next) =>
//        {
//            Console.WriteLine("Student Branch");
//            Console.WriteLine(context.Request.PathBase);
//            Console.WriteLine(context.Request.Path);

//            await next();
//        });

//        student.Map("/danzer", danzer =>
//        {
//            danzer.Run(async context =>
//            {
//                Console.WriteLine("Danzer Branch");
//                Console.WriteLine(context.Request.PathBase);
//                Console.WriteLine(context.Request.Path);

//                await context.Response.WriteAsync("Done");
//            });
//        });
//    });
//});


//-------------------- Serilog detail overview-------------------
/*
 ASP.NET Core ka ILogger abstraction hai. Serilog actual logging provider hai. Serilog me logs ko bahut saari jagah store kar sakte ho, jise Sink bolte hain.

Sink Kya Hota Hai?

Sink = Log ko kaha save karna hai.

Log.Information("User Login");

Ye log kaha jayega?

Console?
Text File?
JSON File?
SQL Server?
MySQL?
PostgreSQL?
Elasticsearch?
Seq?
Azure?

Ye Sink decide karta hai.

1. Console Logging

Package:

Serilog.AspNetCore
Serilog.Sinks.Console
config.WriteTo.Console();

Output:

[10:20:15 INF] User Logged In

Development me sabse jyada use hota hai.

2. Text File Logging

Package:

Serilog.Sinks.File
config.WriteTo.File("logs/log.txt");

Output:

2026-06-22 10:20:15 User Login
2026-06-22 10:21:00 Product Created
3. JSON File Logging

Text file ki jagah JSON format me.

Package:

Serilog.Sinks.File
config.WriteTo.File(
    new Serilog.Formatting.Json.JsonFormatter(),
    "logs/log.json");

Output:

{
  "Timestamp":"2026-06-22T10:20:15",
  "Level":"Information",
  "MessageTemplate":"User Login"
}
Kab Use Kare?

Agar ELK Stack, Splunk, Monitoring Tool log read kare.

Machine-friendly format.

4. Daily File Logging

Industry me ek hi file me saal bhar ke logs nahi rakhte.

config.WriteTo.File(
    "logs/log-.txt",
    rollingInterval: RollingInterval.Day);

Files:

logs/
 ├── log-2026-06-20.txt
 ├── log-2026-06-21.txt
 ├── log-2026-06-22.txt

Har din new file.

5. Monthly File Logging
config.WriteTo.File(
    "logs/log-.txt",
    rollingInterval: RollingInterval.Month);

Output:

log-2026-06.txt
log-2026-07.txt
6. Hourly Logging
rollingInterval: RollingInterval.Hour

Output:

log-2026-06-22-10.txt
log-2026-06-22-11.txt
7. SQL Server Logging

Package:

Serilog.Sinks.MSSqlServer
config.WriteTo.MSSqlServer(
    connectionString,
    sinkOptions);
Database me Logs Kaise Store Hote Hain?

Table:

Logs

Columns:

Id
Message
Level
Timestamp
Exception
Properties

Data:

Id	Level	Message	Timestamp
1	Info	User Login	22-06-2026
2	Error	DB Failed	22-06-2026
Kya Database Me Day Wise Log Ban Sakta Hai?
Option 1 (Recommended)

Ek hi table.

Logs

Date filter lagao:

SELECT *
FROM Logs
WHERE CAST(TimeStamp AS DATE)
='2026-06-22'

Industry me mostly yehi.

Option 2

Month Wise Table

Logs_June
Logs_July

Recommended nahi.

Management mushkil ho jata hai.

Option 3

Table Partitioning

Large systems me.

Logs
  ├─ June Partition
  ├─ July Partition
  ├─ August Partition

User ko ek hi table dikhegi.

SQL Server internally partition karega.

Millions/Billions logs ke liye.

8. MySQL Logging

Package:

Serilog.Sinks.MySQL
config.WriteTo.MySQL(...);
9. PostgreSQL Logging

Package:

Serilog.Sinks.PostgreSQL
config.WriteTo.PostgreSQL(...);
10. Elasticsearch Logging

Package:

Serilog.Sinks.Elasticsearch

Logs search kar sakte ho.

Error
Warning
UserId=5
Date Range

Sab instant search.

Large companies use karti hain.

11. Seq Logging (Bahut Popular)

Package:

Serilog.Sinks.Seq
config.WriteTo.Seq(
    "http://localhost:5341");

Output:

Browser me dashboard.

Info
Warning
Error
Critical

Search bhi kar sakte ho.

Development aur production dono me useful.

12. Multiple Sinks Ek Sath

Sabse important.

Ek log multiple jagah ja sakta hai.

config
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .WriteTo.MSSqlServer(
        connectionString,
        sinkOptions);

Ab ek log:

_logger.LogInformation("User Login");

Jayega:

✅ Console

✅ File

✅ Database

Teeno me ek sath.

Real Industry Setup

Small Project:

Console + File

Medium Project:

Console + Daily File + Seq

Enterprise Project:

Console
+ Daily JSON File
+ Seq
+ Elasticsearch

Database me logging usually sirf business audit logs ke liye karte hain. General application logs ko directly SQL Server me bharna production me har jagah recommended nahi hota, kyunki log volume bahut zyada ho sakta hai.

Yaad Rakhne Wali Baat

ILogger → Interface

ILogger<ProductController>

Serilog → Provider

builder.Host.UseSerilog(...)

Sink → Storage Destination

Console
File
JSON File
SQL Server
MySQL
PostgreSQL
Seq
Elasticsearch
Azure

RollingInterval decide karta hai ki new file kab create hogi:

Day     -> Roz new file
Month   -> Har month new file
Hour    -> Har hour new file
Year    -> Har year new file
Infinite-> Ek hi file
 
 */