using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Exceptions;

namespace Todo.Api.ExceptionHandlers;
    

public class DomainExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler 
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    { 
        if(exception is not DomainException domainException)
            return false;   
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Erro de Validação",
                    Detail = domainException.Message
                }
            }
        );
    }
}