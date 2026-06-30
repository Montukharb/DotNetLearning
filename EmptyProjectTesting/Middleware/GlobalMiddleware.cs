namespace EmptyProjectTesting.Middleware
{
    public class GlobalMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    ErrorMessage = ex.Message
                });

            }
        }
    }
}
