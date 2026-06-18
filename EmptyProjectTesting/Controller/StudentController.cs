using EmptyProjectTesting.Entites;
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
        //[HttpGet("/detail",Name= "Anothertest")] ASP.NET Core route name "GetStudentById" se URL generate kar deta hai.
        //Route Generate using action name
        /*
         [HttpGet("user/{id}",Name = "ActionName")]
         abhi action name se same controller ka ander khi bhi another action ka route get kar sakte hai 
         route ma parameter hai ya another parameters hai unki value deni jaruri hai nahi to url generate nahi hoga optional hai not required
         
         --------------

         There are there ways to get url
         1.Url.RouteUrl("RouteName"); // isme action name required hai
         2.Url.Action("ActionName", "ControllerName"); // actionname and controllername se 
         3.LinkGenerator.GetPathByName("RouteName"); inteeno ka example
        ---------------
        1. example
         
         [HttpGet("student/{id}", Name = "GetStudent")]
         public IActionResult GetStudent(int id)
         {
             return Ok();
         }
         
         [HttpGet("test")]
         public IActionResult Test()
         {
             string? url = Url.RouteUrl(
                 "GetStudent",
                 new { id = 10 }
             );
         
             return Ok(url);
         }
        -----------------
        2. example
        [Route("api/[controller]")]
        public class StudentController : ControllerBase
        {
            [HttpGet("{id}")]
            public IActionResult GetStudent(int id)
            {
                return Ok();
            }

            [HttpGet("test")]
            public IActionResult Test()
            {
                string? url = Url.Action(
                    "GetStudent",
                    "Student",
                    new { id = 10 }
                );

                return Ok(url);
            }
        }

      3. LinkGenerator.GetPathByName()

       Ye service hai jo Controller aur Minimal API dono mein kaam karti hai.
       
       Program.cs
       
       builder.Services.AddRouting(); //abhi need nahi dot net newer version ma auto add hoti hai 
       
       Controller:
       
       [HttpGet("student/{id}", Name = "GetStudent")]
       public IActionResult GetStudent(int id)
       {
           return Ok();
       }
       
       Inject:
       
       private readonly LinkGenerator _linkGenerator;
       
       public StudentController(LinkGenerator linkGenerator)
       {
           _linkGenerator = linkGenerator;
       }
       
       Use:
       
       [HttpGet("test")]
       public IActionResult Test()
       {
           string? url = _linkGenerator.GetPathByName(
               "GetStudent", //ye routename hai
               new { id = 10 } //auske parameter
           );
       
           return Ok(url);
       }
       
       Output
       /student/10

        --------------

        Minimal API Example
        app.MapGet("/student/{id}", (int id) =>
        {
            return Results.Ok();
        })
        .WithName("GetStudent");
        app.MapGet("/test", (LinkGenerator linkGenerator) =>
        {
            var url = linkGenerator.GetPathByName(
                "GetStudent",
                new { id = 5 });
        
            return Results.Ok(url);
        });
        
        Output:
        
        /student/5
         */
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _studentService.GetAllStudents();
            var ur = Url.RouteUrl("Anothertest");
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Student student)
        {
            if(student == null)
            {
                return BadRequest("Null object n");
            }
            var res = await _studentService.AddStudent(student);
            return Ok(res);
        }
    }
}
