using Microsoft.AspNetCore.Mvc;
using Api.Models.DTO.ApiResult;
using Api.Models.DTO.Request;
using Api.Models.DTO.Response;
using Api.Services.Interface;

namespace Api.Endpoints;

public static class AuthEndpoints
{
    public static void ConfigureAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/api/login", Login)
            .WithName("Login")
            .Accepts<LoginRequest>("application/json")
            .Produces<ApiResponse<LoginResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Log in")
            .WithTags("Auth")
            .WithOpenApi(options =>
            {
                options.Summary = "Log in";
                return options;
            });

        app.MapPost("/api/register", Register)
            .WithName("Register")
            .Accepts<RegisterRequest>("application/json")
            .Produces<ApiResponse<UserResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Register a new user")
            .WithTags("Auth")
            .WithOpenApi(options =>
            {
                options.Summary = "Register a new user";
                return options;
            });
    }

    private async static Task<IResult> Login(ILogger<Program> _logger, IAuthService _authService, [FromBody] LoginRequest request)
    {
        _logger.LogInformation($"Sign in for user: '{request.Username}'");
        var response = await _authService.Login(request);
        return Results.Ok(ApiResponse<LoginResponse>.Success(response));
    }

    private async static Task<IResult> Register(ILogger<Program> _logger, IAuthService _authService, [FromBody] RegisterRequest request)
    {
        _logger.LogInformation($"Register new user for: '{request.Username}'");
        var response = await _authService.Register(request);
        return Results.Ok(ApiResponse<UserResponse>.Success(response));
    }
}
