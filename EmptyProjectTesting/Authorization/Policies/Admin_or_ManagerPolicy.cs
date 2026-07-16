using EmptyProjectTesting.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace EmptyProjectTesting.Authorization.Policies
{
    public static class Admin_or_ManagerPolicy
    {
        public static void AdminManagerPolicy(AuthorizationOptions options)
        {
            options.AddPolicy("SpecialPolicy", policy =>
            {
                policy.RequireRole("Teacher");
                policy.RequireClaim("Gender", "Male");
            });

            options.AddPolicy("Admin_or_Manager_policy", policy =>
            {
                policy.AddRequirements(requirements: new Admin_or_Manager("Egypt"));
            });
        }
    }
}
