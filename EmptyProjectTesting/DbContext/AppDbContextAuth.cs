using EmptyProjectTesting.Entites.Auth;
using EmptyProjectTesting.Entity_Configuration_Auth;
using Microsoft.EntityFrameworkCore;

namespace EmptyProjectTesting.DbContexts
{
    public class AppDbContextAuth : DbContext
    {
        public AppDbContextAuth(DbContextOptions<AppDbContextAuth> options) : base(options) { }

        public DbSet<User> Users => Set<User>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContextAuth).Assembly);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
