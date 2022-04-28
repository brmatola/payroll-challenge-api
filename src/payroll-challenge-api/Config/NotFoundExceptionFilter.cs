using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace payroll_challenge_api.Config;

public class NotFoundExceptionFilter : IExceptionFilter
{
    private readonly ILogger<NotFoundExceptionFilter> _logger;

    public NotFoundExceptionFilter(ILogger<NotFoundExceptionFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not NotFoundException e) return;

        _logger.LogError("Client error during request {request}: {message}", context.ActionDescriptor, e.Message);
        context.Result = new NotFoundResult();
    }
}