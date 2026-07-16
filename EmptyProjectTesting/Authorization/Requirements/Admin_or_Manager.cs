using Microsoft.AspNetCore.Authorization;

namespace EmptyProjectTesting.Authorization.Requirements
{
    public class Admin_or_Manager : IAuthorizationRequirement
    {
        public string Country { get; init; }
        public Admin_or_Manager(string country)
        {
            Country = country;
        }
    }
}
