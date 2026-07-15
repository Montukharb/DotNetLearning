using Microsoft.AspNetCore.Authorization;

namespace EmptyProjectTesting.Custom_Auth
{
    public class AdminOrIndiaRequirement:IAuthorizationRequirement
    {

    }

    public class AdminOrIndiaHandler : AuthorizationHandler<AdminOrIndiaRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrIndiaRequirement requirement)
        {
            //throw new NotImplementedException();
            bool isAdmin = context.User.IsInRole("Admin");

            bool isIndia =
                context.User.HasClaim("country", "india");

            if (isAdmin || isIndia)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
