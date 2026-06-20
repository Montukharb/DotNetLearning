using EmptyProjectTesting.ControllerActionFilter;
using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmptyProjectTesting.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    /*
     apply karne ka teen types hai 
    1. [TypeFilter(typeof(NotbookActionFilter),Arguments = new object[] { "stringvalue",10 })]
    2. [ServiceFilter(typeof(NotbookActionFilter))]
    3. [Log] -> ye bas AttributeActionFilter par hi use hoga or [LogAttributeFilter] proper class name bhi de sakte hai koi propblem nahi hai compiler automatic remove kar deta hai
      [Log] -> isme eak problem hai jab bhi ActionFilterAttribute agar koi constructor ma dependency inject kar raha ho ya fir Iloger service use kar raha to error dega kyu ki attribute construtor 
     */

    //[TypeFilter(typeof(NotbookActionFilter),Arguments = new object[] { "stringvalue",10 })] //arguement sequence me hi pass hogi type filter ma di create karne ki need nahi hai apne app kar le ga
    //[ServiceFilter(typeof(NotbookActionFilter))] service filter use karne se phele di registration karna mandatory ha program.cs file ma ex = builder.service.addScoped<NotbookActionFilter>();

    /*
      execution order 
      1 Global Level Filter
      2.Controller Level Filter
      3.Action Level Filter
     */
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
