using EmptyProjectTesting.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace EmptyProjectTesting.Authorization.Handlers
{
    public class Admin_or_Manager_Handler : AuthorizationHandler<Admin_or_Manager>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Admin_or_Manager requirement)
        {
            //throw new NotImplementedException();
            var role = context.User.IsInRole("Admin,Manager");
            var claim = context.User.HasClaim("Country", "Egypt");
            if(role || claim && requirement.Country == "Egypt")
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
