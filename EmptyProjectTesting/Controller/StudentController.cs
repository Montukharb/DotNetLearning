using EmptyProjectTesting.Entites;
using EmptyProjectTesting.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace EmptyProjectTesting.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
       
        private readonly IStudentServices _studentService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IStudentServices studentService ,IConfiguration configuration,IWebHostEnvironment env,ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _configuration = configuration;
            _env = env;
            _logger = logger;
        }
        [HttpGet("testEnv")] //without slash controller + action both
        //[HttpGet("/testEnv")] // /testEnv starting ma slash use karne par absolute path bane ga apne app ko controller se divide kar de ga route bane              http://localhost:5272/testEnv
        public IActionResult TestingEnvironment(IStudentServices stuService)
        {
            string environmentName= _env.EnvironmentName;
            var rootPath = _env.WebRootPath;
            _logger.LogInformation("Environment Name {Name}",environmentName);
            return Ok(new
            {
                EnvironmentName = environmentName,
                WebRootPath = rootPath,
                ProjectRootPath = _env.ContentRootPath
            });
        }


        //[HttpGet("/detail",Name= "Anothertest")] ASP.NET Core route name "GetStudentById" se URL generate kar deta hai.
        //Route Generate using action name
        /*
         [HttpGet("user/{id}",Name = "ActionName")]
         abhi action name se same controller ka ander khi bhi another action ka route get kar sakte hai 
         route ma parameter hai ya another parameters hai unki value deni jaruri hai nahi to url generate nahi hoga optional hai not required
         
         --------------
        complete application me khi se bhi route create kar sakte hai
         There are there ways to get url
         1.Url.RouteUrl("RouteName"); // isme route name required hai
         2.Url.Action("ActionName", "ControllerName"); // actionname and controllername se 
         3.LinkGenerator.GetPathByName("RouteName"); inteeno ka example
        ---------------
        1. example
         
         [HttpGet("student/{id}", Name = "getstudent")]
         public IActionResult GetStudent(int id)
         {
             return Ok();
         }
         
         [HttpGet("test")]
         public IActionResult Test()
         {
             string? url = Url.RouteUrl(
                 "getstudent",
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
        [HttpGet] //get all records
        public async Task<IActionResult> Get()
        {
            var res = await _studentService.GetAllStudents();
            var ur = Url.RouteUrl("Anothertest");
            var dburl = _configuration.GetConnectionString("DefaultConnection");
            //nested ma : use hoge
            var appname = _configuration["MySetting:AppName"];
            Console.WriteLine(dburl);
            Console.WriteLine(appname);
            string str = "hello world";

            byte[] data = new byte[str.Length];
            data = Encoding.UTF8.GetBytes(str);
            //return File(data,"text/plain","downloadname.txt");
            //3 overload ma browser auto download kar de ga agar pass kiya toh.

            /*PDF: "application/pdf"Excel(.xlsx): "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"Word (.docx): "application/vnd.openxmlformats-officedocument.wordprocessingml.document"Text(.txt): "text/plain"CSV: "text/csv"ZIP: "application/zip"Images: "image/jpeg" या "image/png"JSON: "application/json"
            
             */
            
            /*
            string filePath = @"C:\MyFiles\LargeReport.pdf";

            // 2.open file as a FileStream
            // 'using' ka use na kare , ASP.NET Core file transfer hone ka bad auto close kar deta hai
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // 3. File ओवरलोड: (Stream, ContentType, DownloadName)
            // 
            return File(stream, "application/pdf", "DownloadedReport.pdf")/
                */

            
            return Ok(res);
        }
        [HttpPost] //Record add
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            //var provider = new FileExtensionContentTypeProvider();
            //if(provider.TryGetContentType("applic.pdf",out string typ))
            //{
            //    typ = V;
            //}
            if (student == null)
            {
                return BadRequest("Null object");
            }
            var res = await _studentService.AddStudent(student);
            return Ok(res);
        }

        [HttpGet("{id:int}")] //record get by id
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _studentService.GetByIdStudent(id);
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound(new { Message = $"Not found record at id = {id}" });
        }

        [HttpDelete("{id:int}")] //record delete by id
        public async Task<IActionResult> DeleteById(int id)
        {
            var res = await _studentService.DeleteByIdStudents(id);
            if (!res)
            {
                return NotFound(new
                {
                    Message = $"Does't exits data id = {id}"
                });
            }
            return Ok(res);
        }
        [HttpPut("{id:int}")] //Complete update record
        public async Task<IActionResult> Put(int id, [FromBody]Student student)
        {
            
            var res = await _studentService.UpdateStudentRecordFullById(id, student);
                       
            if (!res)
            {
                return NotFound(new
                {
                    Message = $"Does't exits data id = {id}"
                });
            }
            return Ok(new
            {
                Message = $"Record updated at id = {id}"
            });
        }
    }

}
