using System.Net;
using KitchenClube.Exceptions;
using WebApplication1.Models;

namespace KitchenClube.CustomExceptionMiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleWare(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try 
            {
                await _next(context);
            }
            catch (Exception error) {

                var response = context.Response;
                response.ContentType = "application/json";

                switch (error) {
                    case BadRequestException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case NoAccessException:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    case NotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }     
                await context.Response.WriteAsync(new ErrorDetails {
                     StatusCode = context.Response.StatusCode,
                     Message = error.Message
                }.ToString());
            }            
        }        
    }
}
