namespace EmptyProjectTesting.Filter
{
    public class DepartmentEndPointFilter :IEndpointFilter
    {
        //ye task register hai already iendpointfilter interface contract complte karna hoga
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //var firstparam = context.GetArgument<>(0);
            

            return await next(context);
        }
    }
}
