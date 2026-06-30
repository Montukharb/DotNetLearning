using EmptyProjectTesting.Race_Condition;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectTesting.Controller.Race_Cond_Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceCondController : ControllerBase
    {
        private readonly RaceProgram _raceProgram;
        public RaceCondController(RaceProgram raceProgram)
        {
            _raceProgram = raceProgram;
        }

        [HttpGet]
        public IActionResult RaceHandlerGet()
        {
            var countValue = _raceProgram.ThreadHandler();
            return Ok(countValue);
        }
    }
}
