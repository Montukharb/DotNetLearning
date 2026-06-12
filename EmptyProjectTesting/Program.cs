using EmptyProjectTesting.DbContexts;
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
app.MapControllers(); //ye route ko map karta hai controller ke action method ke sath jese ki http get post put delete etc.
app.MapGet("/", () => "Hello, World " + appName);
  
app.Run();

