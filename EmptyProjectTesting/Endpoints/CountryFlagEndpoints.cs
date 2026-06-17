using EmptyProjectTesting.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectTesting.Endpoints
{
    public static class CountryFlagEndpoints
    {
        public static void MapCountryFlagEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/countryflag");

            group.MapGet("", ([FromServices] AppDbContext context) =>
            {
                
            });
        }
    }
}
