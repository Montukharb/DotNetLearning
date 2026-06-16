using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmptyProjectTesting.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotebookController : ControllerBase
    {
        private readonly AppDbContext _context;
        public NotebookController(AppDbContext context) //context ma app dbcontext ka object ayega automatically dependency injection ke through
        {
            _context = context;
        }
        [HttpGet("Allproducts/{id}")]
        public IActionResult Get()
        {
            return Ok("All products");
        }

        [HttpPost]
        public IActionResult Post()
        {
            SpiralNotebook notebook = new SpiralNotebook
            {
                
                Name = "Spiral Notebook",
                Email = "spiralNotebook@gmail.com",
                Address = "123 Main St, Anytown, USA",
                Description = "A high-quality spiral notebook for all your note-taking needs."

            };
            _context.SpiralNotebooks.Add(notebook);
            _context.SaveChanges();
            return Ok("Product created");
        }
       

    }
}
