using Microsoft.AspNetCore.Identity;

namespace EmptyProjectTesting.IDENTITY
{
    public class ApplicationUser : IdentityUser
    {
        //IdentityUser inbuild properties
        /*
         Id,
         UserName,
         NormalizedUserName,
         Email,
         NormalizedEmail,
         EmailConfirmed,
         PasswordHash,
         SecurityStamp,
         ConcurrencyStamp,
         PhoneNumber,
         PhoneNumberConfirmed,
         TwoFactorEnabled,
         LockoutEnd,
         LockoutEnabled,
         AccessFailedCount
         */

        //Note Here we can add our own custom properties like

        public required string FirstName { get; set; }
        public string? LastName { get; set; }
    }
  
}
