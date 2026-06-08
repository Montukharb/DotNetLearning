using Batmen.Entity;
using Microsoft.EntityFrameworkCore;

namespace Batmen.Controllers.Context
{
    public class AppDbContext :DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> option):base(option)
        {

        }

        DbSet<BatmanSkill>BatmanSkills { get; set; }
    }
}
