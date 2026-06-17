using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.Middleware;
using EmptyProjectTesting.Repository;
using EmptyProjectTesting.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var dbConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var appName = builder.Configuration["MySetting:AppName"];

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(dbConnection));

//First always prefer to register services and repository then controller
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentServices, StudentService>();
builder.Services
       .AddControllers()
       .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.ReferenceHandler =
               ReferenceHandler.IgnoreCycles;
       });
builder.Services.AddControllers(); //controller related service register karta hai

var app = builder.Build();
app.Use((context, next) =>
  {
      //context.Response.WriteAsync("Use middleware First");
      Console.WriteLine("use middleware first");
      return next();

  }
);
//global level exception handler 4 ways
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


app.MapControllers(); //ye route ko map karta hai controller ke action method ke sath jese ki http get post put delete etc.
app.MapGet("/", () => "Welcome to asp.net core web api " + appName); //minimal api example
//wild card routes handled by inbuild minimal api routing feature ye internall routing system me register hota hai hamesha last ma place hoga iss se phele minimal api use kar sakte hai 
//app.Map("/{*path}", branch => { }); // ye bhi unknown route handle kar sakta hai rarely use hota hai

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