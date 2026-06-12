using Microsoft.EntityFrameworkCore;
using WebApplicationBackend.Context;
using WebApplicationBackend.Entities;

public partial class Program
{
    public static void Main(string[] args)
    {
        // Create a builder for the web application, which will be used to configure services and the app itself.
        //createbuilder args ma appsetting file ka path de sakte hai ya environment variable bhi de sakte hai jisme hum custom setting rakhte hai 
        var builder = WebApplication.CreateBuilder(args);

        var appName = builder.Configuration["MySettings:AppName"]; //custom setting access from appsetting.json file Colon (:)means nested

        // database connection string access from appsetting.json file
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Add services to the container. means controller add ho jayenge aur hum unko use kar sakte hai application me
        builder.Services.AddControllers();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        //all services are registered in the container and we can use those services in the application 
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        // middlewares
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }


}