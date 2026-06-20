using System.Diagnostics;
namespace EmptyProjectTesting.Filter
{
    public class DepartmentEndPointFilter : IEndpointFilter
    {
        //ye task register hai already iendpointfilter interface contract complte karna hoga
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //var firstparam = context.GetArgument<>(0);
            //calculate execution time
            var sw = Stopwatch.StartNew();
            var code = context.GetArgument<string>(0);
            if (code == null)
            {
                return TypedResults.NoContent();
            } 
            var res = await next(context);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            return res; //ye line jab execute hogi endpoint return kare ga 
        }
    }
    //public class StudentEndPointFilter : IEndpointFilter
    //{
    //    public ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}

/*
Use Case 1: Validation

Request:

{
   "name":""
}

Filter:

if(string.IsNullOrWhiteSpace(student.Name))
{
    return Results.BadRequest();
}

Endpoint tak jaane hi nahi diya.

Flow:

Request
   ↓
Validation Filter
   ↓
BadRequest
   ↓
Response
Use Case 2: Logging

Har request log karni hai.

Console.WriteLine(
    "Endpoint Called");

20 endpoints me likhoge?

Nahi.

Filter me likho.

public class LoggingFilter
:IEndpointFilter
{
    public async ValueTask<object?>
    InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        Console.WriteLine("Before");

        var result =
            await next(context);

        Console.WriteLine("After");

        return result;
    }
}

Output:

Before
Endpoint
After
Use Case 3: Execution Time

Kitna time laga?

var sw = Stopwatch.StartNew();

var result =
    await next(context);

sw.Stop();

Console.WriteLine(sw.ElapsedMilliseconds);
Use Case 4: Custom Authentication

Header check.

var token =
context.HttpContext
       .Request
       .Headers["Token"];

Token nahi?

return Results.Unauthorized();

Endpoint skip.

Use Case 5: Audit

Kisne record delete kiya.

UserId
DateTime
Endpoint Name

Database me save.

Use Case 6: Response Modification

Endpoint:

return Results.Ok(student);

Filter:

var result =
    await next(context);

Response ko wrap bhi kar sakte ho.Use Case 1: Validation

Request:

{
   "name":""
}

Filter:

if(string.IsNullOrWhiteSpace(student.Name))
{
    return Results.BadRequest();
}

Endpoint tak jaane hi nahi diya.

Flow:

Request
   ↓
Validation Filter
   ↓
BadRequest
   ↓
Response
Use Case 2: Logging

Har request log karni hai.

Console.WriteLine(
    "Endpoint Called");

20 endpoints me likhoge?

Nahi.

Filter me likho.

public class LoggingFilter
:IEndpointFilter
{
    public async ValueTask<object?>
    InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        Console.WriteLine("Before");

        var result =
            await next(context);

        Console.WriteLine("After");

        return result;
    }
}

Output:

Before
Endpoint
After
Use Case 3: Execution Time

Kitna time laga?

var sw = Stopwatch.StartNew();

var result =
    await next(context);

sw.Stop();

Console.WriteLine(sw.ElapsedMilliseconds);
Use Case 4: Custom Authentication

Header check.

var token =
context.HttpContext
       .Request
       .Headers["Token"];

Token nahi?

return Results.Unauthorized();

Endpoint skip.

Use Case 5: Audit

Kisne record delete kiya.

UserId
DateTime
Endpoint Name

Database me save.

Use Case 6: Response Modification

Endpoint:

return Results.Ok(student);

Filter:

var result =
    await next(context);

Response ko wrap bhi kar sakte ho.
*/