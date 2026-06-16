namespace EmptyProjectTesting.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next) //Asp.Net send automatic next middleware reference 
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) //Invoke is naming convention here
        {
            string @path = context.Request.Path;
            Console.WriteLine("Incoming request path = " + @path);
            await _next(context); //call next pipeline middleware and send context data
        }

    }
}
