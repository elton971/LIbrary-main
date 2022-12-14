using System.Net;
using Aplication.Errors;
using Newtonsoft.Json;

namespace Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e, _logger);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
    {
        object errors = null;
        switch (ex)
        {
            case RestException re:
            {
                logger.LogError(ex,"REST ERROR");
                errors = re.Errors;
                context.Response.StatusCode = (int)re.Code;
            }
                break;
            case Exception e:
            {
                logger.LogError(ex,"SERVER ERROR");
                errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
                break;
        }

        context.Response.ContentType = "application/json";
        if (errors != null)
        {
            var result = JsonConvert.SerializeObject(new
            {
                errors
            });

            await context.Response.WriteAsync(result);
        }
    }
}