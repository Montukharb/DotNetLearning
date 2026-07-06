using EmptyProjectTesting._Mutex;
using EmptyProjectTesting.Race_Condition;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectTesting.Controller.Race_Cond_Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceCondController : ControllerBase
    {
        private readonly RaceProgram _raceProgram;
        private readonly MutexSample _mutexSample;
        public RaceCondController(RaceProgram raceProgram, MutexSample mutex)
        {
            _raceProgram = raceProgram;
            _mutexSample = mutex;
        }

        [HttpGet("creater/")]
        public IActionResult RaceHandlerGet()
        {
            var countValue = _raceProgram.ThreadHandler();
            return Ok(countValue);
        }
        [HttpGet("solver/")]
        public IActionResult RaceSolverGet()
        {
            var countValue = _raceProgram.ThreadHandler_RaceCondSolver();
            return Ok(countValue);
        }
        [HttpGet("sameosthread/")]
        public IActionResult SameOsThreadGet()
        {
            SolveRaceCondition.SameOsThread();
            return Ok(true);
        }
        [HttpGet("mutex/")]
        public async Task<IActionResult> MutexGet()
        {
            var res = await _mutexSample.MutexExample();
            //(string message, bool status) = await _mutexSample.MutexExample(); //better syntax
            if (!res.status)
            {
                return BadRequest(res);
            }
            return Ok(res.msg + " " + res.status);
        }
    }
}
