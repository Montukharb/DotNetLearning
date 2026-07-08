using EmptyProjectTesting._Mutex;
using EmptyProjectTesting.Concurrent_Collections;
using EmptyProjectTesting.Race_Condition;
using EmptyProjectTesting.Reader_Writer_LockSlim;
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
        private readonly ReaderWriterLockSlimSample _readerWriterLockSlimSample;
        public RaceCondController(RaceProgram raceProgram, MutexSample mutex, ReaderWriterLockSlimSample readerWriterLockSlimSample)
        {
            _raceProgram = raceProgram;
            _mutexSample = mutex;
            _readerWriterLockSlimSample = readerWriterLockSlimSample;
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
        [HttpGet("readwritelock/")]
        public async Task<IActionResult> ReadWritelockGet()
        {
            var res = await _readerWriterLockSlimSample.ReadWriteLockSlimExample();
            return Ok(res);
        }

        [HttpGet("concurrentCollection/")]
        public async Task<IActionResult> ConcurrentCollectionGet([FromServices] ConcurrentCollectionSample ccs)
        {
            var res = ccs.ConcurrentDictionaryExample();
            return res ? Ok(res) : BadRequest(res);
        }

        [HttpGet("blockingqueue/")]
        public async Task<IActionResult> BlockingQueueGet([FromServices] ConcurrentCollectionSample ccs)
        {
            var res = ccs.BlockingCollectionExample();
            return res ? Ok(res) : BadRequest(res);

        }
    }
}
