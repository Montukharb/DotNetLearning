namespace EmptyProjectTesting.Middleware
{
    public class CustomMiddleware2
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware2(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            Console.WriteLine("Custome class middleware 2");
            return _next(context);
        }
    }
}
