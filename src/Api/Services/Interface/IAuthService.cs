using Api.Models.DTO.Request;
using Api.Models.DTO.Response;

namespace Api.Services.Interface;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginRequest request);
    Task<UserResponse> Register(RegisterRequest request);
}
