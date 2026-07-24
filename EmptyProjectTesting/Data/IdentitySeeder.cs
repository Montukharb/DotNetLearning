using Microsoft.AspNetCore.Identity;

namespace EmptyProjectTesting.Data
{
    public static class IdentitySeeder
    {
        public static async Task IdentitySeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Admin", "User", "Guest", "Employee", "Manager", "SuperAdmin" };

            foreach (var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
