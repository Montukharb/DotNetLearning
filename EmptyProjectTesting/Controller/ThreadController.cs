using EmptyProjectTesting.Thread_s;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectTesting.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThreadController : ControllerBase
    {
        private readonly ThreadPoolSample _threadPool;

        public ThreadController(ThreadPoolSample threadpool)
        {
            _threadPool = threadpool;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _threadPool.ThreadPoolMain();
            return Ok("Thread Controller is working!");
        }
    }
}
