using Elastic.Serilog.Sinks;
using EmptyProjectTesting.ControllerActionFilter;
using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.Endpoints;
using EmptyProjectTesting.Middleware;
using EmptyProjectTesting.Repository;
using EmptyProjectTesting.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Serilog.Sinks.MSSqlServer;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var dbConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var appName = builder.Configuration["MySetting:AppName"];

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(dbConnection));

//First always prefer to register services and repository then controller
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentServices, StudentService>();

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

    config.WriteTo.MSSqlServer(dbConnection,sinkOptions:SinkOptions); //data base ma logs save hogi
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
       .AddControllers()
       .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.ReferenceHandler =
               ReferenceHandler.IgnoreCycles;
       }); //include se cycle banti hai unko remove karta hai kai bar error bhi de sakta hai better hoga dto use kare
builder.Services.AddScoped<FlagActionFilter>();//ServiceFilter ma Registration karna padta hai typeFilter ma need nahi hai

//department action filter ab global level par add ho gaya hai filter complete app par apply hoga ab
builder.Services.AddControllers(options => { options.Filters.Add<DepartmentActionFilter>(); });

builder.Services.AddControllers(); //controller register karta hai all

var app = builder.Build();
app.Use((context, next) =>
  {
      //context.Response.WriteAsync("Use middleware First");
      Console.WriteLine("use middleware first");
      return next();

  }
);
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
//        context.Response.ContentType = "application/json";
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
app.UseStatusCodePages();
app.UseHttpsRedirection();
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
