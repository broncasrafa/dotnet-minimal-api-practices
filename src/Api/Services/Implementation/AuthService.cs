using Api.Data.Repository.Interface;
using Api.Models.DTO.Request;
using Api.Models.DTO.Response;
using Api.Models.Entity;
using Api.Models.Exceptions;
using Api.Models.Extensions;
using Api.Services.Interface;
using AutoMapper;

namespace Api.Services.Implementation;

public class AuthService(ILogger<AuthService> _logger, IMapper _mapper, IAuthRepository _authRepository, IJwtService _jwtService, IPasswordHasher _passwordHasher) : IAuthService
{
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        // criptografar a senha para conferir na base
        var user = await _authRepository.LoginAsync(request.Username)
                                        .OrElseThrowsAsync(new SignInErrorException());

        bool isPasswordVerified = _passwordHasher.Verify(request.Password, user.Password);
        if (!isPasswordVerified) throw new SignInErrorException();

        // obter o token JWT
        var token = _jwtService.GenerateToken(user);

        var authenticatedUser = new LoginResponse { 
            User = _mapper.Map<UserResponse>(user),
            Token = token 
        };

        return authenticatedUser;
    }

    public async Task<UserResponse> Register(RegisterRequest request)
    {
        var isUniqueUser = _authRepository.IsUniqueUser(request.Username);
        if (!isUniqueUser) throw new UserAlreadyExistsException(request.Username);

        var localUser = _mapper.Map<LocalUser>(request);
        localUser.Password = _passwordHasher.Hash(request.Password);

        var newUser = await _authRepository.Register(localUser);
        return _mapper.Map<UserResponse>(newUser);
    }
}
