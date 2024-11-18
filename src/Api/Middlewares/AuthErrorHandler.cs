using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares;

public class AuthErrorHandler
{
    public static async Task HandleAuthError(HttpContext context, int statusCode)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Instance = context.Request.Path,
            Title = statusCode switch
            {
                StatusCodes.Status401Unauthorized => "Você não está autenticado na API.",
                StatusCodes.Status403Forbidden => "Acesso negado.",
                _ => "Erro de autenticação."
            },
            Detail = statusCode switch
            {
                StatusCodes.Status401Unauthorized => "Por favor, envie um token válido no cabeçalho Authorization da requisição.",
                StatusCodes.Status403Forbidden => "Você não tem permissão para acessar este recurso.",
                _ => "Entre em contato com o suporte para mais informações."
            }
        };

        // Log opcional
        var logger = context.RequestServices.GetRequiredService<ILogger<AuthErrorHandler>>();
        logger.LogError("Auth Error ({StatusCode}): {Path}", statusCode, context.Request.Path);

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
