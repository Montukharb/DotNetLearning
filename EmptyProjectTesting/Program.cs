    using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var appName = builder.Configuration["MySetting:AppName"];

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(dbConnection));
builder.Services.AddControllers(); //controller related service register karta hai

var app = builder.Build();
app.Use((context, next) =>
  {
      //context.Response.WriteAsync("Use middleware First");
      Console.WriteLine("use middleware first");
      return next();

  }
);

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
app.MapGet("/", () => "Hello, World " + appName); //minimal api example

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