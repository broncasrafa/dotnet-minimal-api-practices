using Api.Models.Entity;

namespace Api.Services.Interface;

public interface IJwtService
{
    string GenerateToken(LocalUser user);
}
