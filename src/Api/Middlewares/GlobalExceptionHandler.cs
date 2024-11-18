using Api.DTO.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace Api.Middlewares;


/// <summary>
/// Middleware de Manipulação de Exceções Globais Personalizado, classe configurada como um middleware no pipeline do ASP.NET Core 
/// que contém a lógica de manipulação de erros personalizada possibilitando que uma variedade de exceções possam ser capturadas
/// por este pipeline.
/// </summary>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails = new ProblemDetails();
        problemDetails.Instance = httpContext.Request.Path;
        problemDetails.Title = exception.Message;        

        if (exception is BaseException ex)
        {
            httpContext.Response.StatusCode = (int)ex.StatusCode;
            problemDetails.Title = ex.Message;
        }

        _logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);
        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);

        return true;
    }
}
