using Elastic.CommonSchema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace EmptyProjectTesting.ControllerActionFilter
{
    /*
     Execution pipeline
      Reqest -> OnActionExecuting() -> ActionMethod -> OnActionExecuted() -> Response
      
     */
    public class DepartmentActionFilter : IActionFilter
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly ILogger<DepartmentActionFilter> _logger;
        public DepartmentActionFilter(ILogger<DepartmentActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //ILogger is an interface used to record application events, errors, warnings, and diagnostic information.
            //ILogger ek contract(interface) hai jo application ke andar hone wali activities ko log file, console, database, Elasticsearch, ya kisi aur storage me save karne ke liye use hota hai.
            _stopwatch.Start();
            _logger.LogInformation("stopwatch start");
//what is placholder in logger
//in logger placholder model binding ka liya hota hai jiska kuch bhi name ho sata ahi {abc} lakin meaningful name hona chaiya 
            int userId = 101;
            string name = "Montu";

            _logger.LogInformation(
                "User Id: {UserId}, User Name: {UserName}",
                userId,
                name);
/*OUTPUT:
User Id: 101, User Name: Montu
             
BENEFIT of placeholder
{
  "Message":"User 101 logged in",
  "UserId":101
}

.elastic kibana me search or indixing ka liya searchable rahta hai
.better performance

*/
            _logger.LogWarning("Warning message");
            _logger.LogError("Error message");
            _logger.LogCritical("critical message");
            _logger.LogTrace("trace message");
            /*
            if find any error in context reqest we can stop here execution like
            context.HttpContext.Response.StatusCode = 404;
            context.HttpContext.Response.WriteAsJsonAsync(new { Message = "Not found" });
            depend hai kis type se execution stop karna hai 
            context.Result = new NotFoundObjectResult("Not found");
            */
            //throw new NotImplementedException();
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            /*
              after action return then execute this method
            now we can change response result and status type as you want change it
             */
            var result = context.Result;
            if (result is OkObjectResult output)
            {
                context.Result = new OkObjectResult(new
                {
                    Message = "Modified result by okActionExecuted Filters",
                    value = output.Value
                });
            }
            _stopwatch.Stop();

            Console.WriteLine($"Time {_stopwatch.ElapsedMilliseconds}");

            //throw new NotImplementedException();
        }
    }
    /*
[ServiceFilter(typeof(LogFilter))]
[ServiceFilter(typeof(ValidationFilter))]
[ServiceFilter(typeof(AuditFilter))]
public class StudentController : ControllerBase
{
}
Before Action:

LogFilter Before
    ↓
ValidationFilter Before
    ↓
Action Method ----------

After Action: ----------

Action Method ----------
    ↓
ValidationFilter After
    ↓
LogFilter After

Yani stack ki tarah:

Filter1 Before
    ↓
Filter2 Before
    ↓
Action
    ↓
Filter2 After
    ↓
Filter1 After

     */

    public class FlagActionFilter : IAsyncActionFilter //async ma eak hi method implement karna hota hai ye hi dono kam karta hai execution start beforeaction next call respose handled
                                                       //preferece de ki di,service,db access all type injection isi ma hi kare ya fir ActionFilterAttribute ma kyu ki ye dono hai asyc await provide karte hai or serivices or di ma async await ka method hote hai normall iactionfilter ma error mile sakte hai async await program me  
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Headers["apiKey"] != 9453678)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Message = "Key not matched"
                });
            }
            var res = await next(); //action called and return desired result

            if (res.Result is OkObjectResult use)
            {

                res.Result = new OkObjectResult(new
                {
                    Message = "ActionExecutionDelegate async data",
                    value = use.Value
                });
            }

            //now we can interfare result modified response rok nahi sakte ab ausme altering kar sakte hai
        }
    }

    /*
Microsoft ne convenience ka liye banayi hai actionFilterAttribute code kam likhna padta hai
abstract method provide karti hai 
IActionFilter IAsyncActionFilter -> internally ye class in dono interface ko implement karti hai 

ActionFilterAttribute ke andar 2 options ha
Sync
OnActionExecuting()
OnActionExecuted()

Async
OnActionExecutionAsync()

Normally ek hi pattern use karna chaiya ya to async or sync.
     */
    public class NotbookActionFilter : ActionFilterAttribute
    {
        // sync methods khe sakte hai IActionFilter ka method hai kyuki at the end implement to ye un dono interface ko kar rahi hai
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Before");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("After");
        }

        //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{

        //    var res = await next();


        //}
    }
}
