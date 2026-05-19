using Microsoft.EntityFrameworkCore;
using WebApplicationBackend.Entities;


namespace WebApplicationBackend.Context
{
    //Step 4-> DbContext means database manager class
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { 
          
        }
        
        //Table representation
       public DbSet<Employee> Employees { get; set; } //Employees is a table name
       
    }
}
