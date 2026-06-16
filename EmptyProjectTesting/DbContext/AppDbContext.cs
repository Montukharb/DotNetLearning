using EmptyProjectTesting.Entites;
using Microsoft.EntityFrameworkCore;

namespace EmptyProjectTesting.DbContexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } //pass nessary db connection options

        //Fluent Api = Entity framework core ma database mapping define karne ke 3 ways hai
        /*
          1. Convention -> ye automatic leta hai name convention ka use kar ke
          2. Data Annotation -> isme property class se phele attribute annotation use hote hai
          3. Fluent Api -> fluent api Advance or better control deti hai isko mainly appdbcontext class ma override kiya jata hai predefine abstract method se
         */
        public DbSet<SpiralNotebook> SpiralNotebooks { get; set; }
        public DbSet<Student> Students { get; set; } //actual table name is Students not Student isko bhi by pass kar sakte hai fluent api se ToTable method se.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpiralNotebook>().HasKey(x => x.Id); //HasKey means set primary key Id Columns 
            //This is normal Configuration inside AppDbContext but in industry standard never set here Entity Configuration create Seprate Configuration Folder and create seprate class for configuration 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly); //auto register karne ka liya hai ef apne app scan kar le configuration class ka configure method.
        }
    }
}
