using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectTesting.Controller
{
    [ApiController]
    [Route("error/[controller]")]
    public class GlobalExceptionControler : ControllerBase
    {
        public IActionResult Error()
        {
            return Problem("Something went wrong");
        }
    }
}
