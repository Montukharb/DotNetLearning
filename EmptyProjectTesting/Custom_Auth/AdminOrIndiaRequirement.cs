using Microsoft.AspNetCore.Authorization;

namespace EmptyProjectTesting.Custom_Auth
{
    public class AdminOrIndiaRequirement : IAuthorizationRequirement //Marker class of requirement 
    {
        //Note : In marker class we don't write any method write only requirement properties

    }

    public class AdminOrIndiaHandler : AuthorizationHandler<AdminOrIndiaRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrIndiaRequirement requirement)
        {
            /*      User
            -------
            | Method      | Work                      |
| ----------- | ------------------------- |
| IsInRole()  | Role check                |
| FindFirst() | First claim               |
| FindAll()   | Same type ke saare claims |
| HasClaim()  | Claim exist karta hai?    |
| Claims      | All claims                |
| Identity    | Login info                |

            -----------
      Resource   ->object
      Requirements   -> current policy all requirements return IEnumerable<IAuthorizationRequirement>
      PendingRequirements -> returns all pending requirements
      HasSucceeded -> returns true or false when required authorization is completed
      HasFailed -> returns true or false when required authorization is failed or not
      Succeed() -> is used to pass the authorization
      Fail() -> is used to fail the authorization
            
             */
            //throw new NotImplementedException();
            bool isAdmin = context.User.IsInRole("Admin");
            var claims = context.User.Claims;
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type = " + claim.Type + " ,Claim Value = " + claim.Value);
            }


            bool isIndia = context.User.HasClaim("country", "india");

            if (isAdmin || isIndia)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
