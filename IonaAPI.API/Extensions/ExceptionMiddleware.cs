using System.Net;
using System.Web.Http;

namespace IonaAPI.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (HttpResponseException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var error = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error."
            };

            await context.Response.WriteAsync(error.ToString());
            logger.LogError("Error", error);
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpResponseException exception)
        {
            var responseException = exception.Response;
            
            if(responseException != null)
            {
                if (responseException.StatusCode == HttpStatusCode.RequestTimeout)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var error = new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Server takes too long to respond. Try again after a minute."
                    };

                    await context.Response.WriteAsync(error.ToString());
                    logger.LogError($"Error: {responseException.StatusCode}", responseException.ReasonPhrase);
                }
                else
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var error = new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error"
                    };

                    await context.Response.WriteAsync(error.ToString());
                    logger.LogError($"Error: {responseException.StatusCode}", responseException.ReasonPhrase);
                }
            }
        }
    }
}
