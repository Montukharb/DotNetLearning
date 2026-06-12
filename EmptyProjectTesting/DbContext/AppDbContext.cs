using EmptyProjectTesting.Entites;
using Microsoft.EntityFrameworkCore;

namespace EmptyProjectTesting.DbContexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<SpiralNotebook> SpiralNotebooks { get; set; }
    }
}
