using EmptyProjectTesting.State_Configuration;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectTesting.Controller.StateBasedController
{
    [ApiController]
    [Route("api/worker")]
    public class WorkerController : ControllerBase
    {
        private readonly State _state;
        private readonly ILogger<WorkerController> _logger;

        public WorkerController(State state, ILogger<WorkerController> logger)
        {
            _state = state;
            _logger = logger;
        }

        [HttpGet("start")]
        public IActionResult StartWorker()
        {
            if (_state.IsEnabled)
            {
                _logger.LogWarning("Worker already in process");
                return BadRequest(new { Message = "Worker already in process state you have stopped first then start new worker" });
            }
            _state.IsEnabled = true;
            _logger.LogInformation("Flag state worker start");
            return Ok("worker Start");
        }

        [HttpGet("stop")]
        public IActionResult StopWorker()
        {
            if (!_state.IsEnabled)
            {
                _logger.LogWarning("Worker already in idle state");
                return BadRequest(new { Message = "Worker already in idle state" });
            }
            _state.IsEnabled = false;
            _logger.LogInformation("Flag state worker idle set");
            return Ok("worker set idle state");
        }
    }
}
