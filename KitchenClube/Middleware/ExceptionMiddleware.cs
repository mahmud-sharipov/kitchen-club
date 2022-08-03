using KitchenClube.Exceptions;

namespace KitchenClube.MiddleWare;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {

            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                IKitchenClubeCustomException customException => (int)customException.StatusCode,
                _ => (int)HttpStatusCode.InternalServerError
            };

            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = error.Message
            }.ToString());
        }
    }
}
