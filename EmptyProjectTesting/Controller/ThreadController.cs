using EmptyProjectTesting.Tasks_Prog;
using EmptyProjectTesting.Thread_s;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectTesting.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThreadController : ControllerBase
    {
        private readonly ThreadPoolSample _threadPool;
        private readonly TaskSample _taskSample;
        private readonly TaskSample2 _taskSample2;

        public ThreadController(ThreadPoolSample threadpool, TaskSample task, TaskSample2 task2)
        {
            _threadPool = threadpool;
            _taskSample = task;
            _taskSample2 = task2;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _threadPool.ThreadPoolMain();
            return Ok("Thread Controller is working!");
        }
        [HttpGet("task/")]
        public IActionResult GetTask()
        {
            _taskSample.TaskMain();
            return Ok("Task Controller is working");
        }
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            string status = _taskSample.StatusExample();
            return Ok(status);
        }
        [HttpGet("iscompleted")]
        public async Task<IActionResult> GetCompleted()
        {
            var res = await _taskSample.IsCompletedEx();
            return Ok(res);
        }
        [HttpGet("tasksamp2/")]
        public async Task<IActionResult> TaskSam2Get()
        {
            var res = await _taskSample2.TaskEx();
            return Ok(res);
        }
        [HttpGet("waitall/")]
        public async Task<IActionResult> WaitAllGet()
        {
            var res = await _taskSample2.WaitAllEx();
            return Ok(res);
        }
    }
}
