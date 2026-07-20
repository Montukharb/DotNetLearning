//using EmptyProjectTesting.Entites;
using EmptyProjectTesting.IDENTITY;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmptyProjectTesting.DbContexts
{
    public class IdentityAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options)
        { }

        //public DbSet<Student> StudentIdentity => Set<Student>();
        //Normal DbContext Still Woring same add additional features in IdentityDbContext


    }
}
