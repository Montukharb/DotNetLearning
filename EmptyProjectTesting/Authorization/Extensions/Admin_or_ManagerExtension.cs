using EmptyProjectTesting.Authorization.Handlers;
using EmptyProjectTesting.Authorization.Policies;
using Microsoft.AspNetCore.Authorization;

namespace EmptyProjectTesting.Authorization.Extensions
{
    public static class Admin_or_ManagerExtension
    {
        public static IServiceCollection AdminOrManagerExtenstion(this IServiceCollection services)
        {
            services.AddAuthorization(Admin_or_ManagerPolicy.AdminManagerPolicy);
            services.AddSingleton<IAuthorizationHandler, Admin_or_Manager_Handler>();

            return services;
        }
    }
}
