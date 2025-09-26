using System.Net;

namespace NZWalksAPI.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger1;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger1, RequestDelegate next)
        {
            this.logger1 = logger1;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                // Log the exception
                logger1.LogError(ex, $"Something went wrong! ErrorId: {errorId} : {ex.Message}");


                // Return a generic error response to the client
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;     // 500 response
                httpContext.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    ErrorId = errorId,
                    Message = "An unexpected error occurred. We're looking into resolving it."
                };
                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}  