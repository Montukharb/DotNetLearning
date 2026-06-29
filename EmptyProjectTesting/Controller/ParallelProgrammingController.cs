using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.ParallelProgramming;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyProjectTesting.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParallelProgrammingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ParallelProgrammingController> _logger;
        private readonly ParallelProgram _parallelProgram;
        public ParallelProgrammingController(AppDbContext contextPool,ILogger<ParallelProgrammingController> logger,ParallelProgram parallelProgram)
        {
            _context = contextPool;
            _logger = logger;
            _parallelProgram = parallelProgram;

        }

        [HttpGet("sequential")]
        public async Task<IActionResult> Sequential()
        {
            _logger.LogInformation("SequentialProgramming service executed");
            _parallelProgram.SequentialMainMethod();

            return Ok("sequentialProgramming");
        }
    }
}
