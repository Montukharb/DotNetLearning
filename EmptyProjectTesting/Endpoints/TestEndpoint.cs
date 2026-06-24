using EmptyProjectTesting.Entites;
using EmptyProjectTesting.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace EmptyProjectTesting.Endpoints
{
    public static class TestEndpoint
    {

        /*filter factory advanced feature hai. Runtime par method inforamation provide karta hai ex = metadata,parameters,returntypes inspect karke dynamically filter create kar sakte hai
         # The main benefit of filter factory 
        1.Before executing action method gather all information about routes parameters data type method name and other infomation
        2.Dynamically create Filter and changed
        3.Conditionally applied filter based on param data type etc.
 Flow:
        Applicatoin start -> FactoryExecuted -> FilterCreate -> RequestCome/Trigger -> FilterExecute(based on condition) -> ActionMethod -> ReverseFilter Execution start;
        Note: factoryContext = provide different types infomation like methodInfo, name, parameters etc
         */
        public static void MapTestEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api2/test/person");
            group.MapGet("{id:int?}/{name:alpha?}", async Task<Results<Ok<Student>, NotFound>> (int? id, [FromQuery] string? name, [FromServices] IStudentServices student,[FromServices] ILogger<Student> logger) =>
            {
                logger.LogInformation("api test {name} user", name);
                var students = await student.GetByIdStudent(id ?? 2);

                if (students is not null)
                {
                    return TypedResults.Ok(students);
                }
                return TypedResults.NotFound();
            }).AddEndpointFilterFactory((factoryContext, next) =>
            {

                var methodInfo = factoryContext.MethodInfo;
                var methodName = factoryContext.MethodInfo.Name;
                ParameterInfo[]? parameters = factoryContext.MethodInfo.GetParameters();
                foreach (var pamtrs in parameters)
                {
                    Console.WriteLine($"Parameter name = {pamtrs.Name}, DataType = {pamtrs.ParameterType}");
                }
                if (parameters?.First()?.ParameterType == typeof(int?))
                {
                    return async context => //filter create
                    {
                        Console.WriteLine("int data-type match firstParameter filter created");
                        Console.WriteLine("Before action");
                        var Action_res = await next(context);
                        Console.WriteLine("After action");
                        return Action_res;

                    };
                }
                
                    return async context => //filter create
                    {
                        Console.WriteLine("Before action2");
                        var Action_res = await next(context);
                        Console.WriteLine("After action2");
                        //if(Action_res is Results<Ok<Student>,NoContent> use)
                        //{
                        //    return TypedResults.Ok(new
                        //    {
                        //        Message = "Modifed of endpiont result in factory filter",
                        //        Data = use.Result
                        //    });
                        //}
                        return Action_res;
                    };
                

                //return async context =>
                //{
                //    Console.WriteLine("Parameter's not match default filter execut");

                //    return await next(context);
                //};
            });

        }
    }
}
