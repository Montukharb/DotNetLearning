using EmptyProjectTesting.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectTesting.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentService;
        public StudentController(IStudentServices studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _studentService.GetAllStudents();
            return Ok(res);
        }
    }
}
