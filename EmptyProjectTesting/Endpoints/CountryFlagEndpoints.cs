using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.Entites;
using EmptyProjectTesting.Filter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmptyProjectTesting.Endpoints
{
    public static class CountryFlagEndpoints
    {
        static CountryFlagEndpoints() { } //first time static member create hone se phele call hoga 
                                          //ya fir object instance create hone se phele no argument
        public static void MapCountryFlagEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/countryflag");
            //group.AddEndpointFilter<DepartmentEndPointFilter>(); // ye grouplevel filter hai 
            group.MapGet("{code}", async Task<Results<Ok<List<CountryFlag>>, NoContent, BadRequest<string>>> (string code, [FromServices] AppDbContext context) =>
            {
                var result = await context.countryFlag.ToListAsync();

                return TypedResults.Ok(result);

            }).AddEndpointFilter(async (context, next) =>
            {
                Console.WriteLine(context.HttpContext.Request.Path);
                //getting parameters from endpoints using getArgument
                string code = context.GetArgument<string>(0);
                if (code is not ("IN" or "US"))
                {
                    return TypedResults.BadRequest("Enter only india and united states coutry code");
                }
                return await next(context);
            }).WithName("CountryFlagEndpoint").AddEndpointFilter<DepartmentEndPointFilter>(); //withname ka use hoga url get karte time 
        }
    }
}

/*
if (code != "IN" && code != "US")
{
    // code neither IN nor US
}
.NET 9 / C# mein pattern matching
if (code is not ("IN" or "US"))
{
    // code neither IN nor US
}

Ya positive check:

if (code is "IN" or "US")
{
    // code is IN or US
}
Multiple values ke liye
if (new[] { "IN", "US", "UK" }.Contains(code))
{
    // matched
}

Ya:

if (!new[] { "IN", "US", "UK" }.Contains(code))
{
    // not matched
}

*/

