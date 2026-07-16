using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectTesting.Custom_Auth
{
    /* Best practice
    Requirement me                                Allowed?   Recommendation
    Properties	                                   ✅	       Recommended
    Constructor                                    ✅	       Recommended
    Read-only data                                 ✅	       Recommended
    Helper methods(pure, no authorization logic)   ✅	       Kabhi-kabhi*/
    public class MinimumAgeRequirement : IAuthorizationRequirement  //Marker Interface use to mark the requirement
    {
        public int MinimumAge { init; get; }

        public MinimumAgeRequirement(int MinimuAge)
        {
            this.MinimumAge = MinimuAge;
        }

    }


    //# Requirement Handler
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        // Here we can use services in constructor

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (requirement.MinimumAge >= 18)
            {
                context.Succeed(requirement);
                //Succeed() controller ko immediately execute nahi kar deta.Agar policy me aur requirements hain, unka evaluation bhi hota hai.
            }
            
            return Task.CompletedTask;
        }
    }

}
