using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Api.Models.Entity;
using Api.Models.Settings;
using Api.Services.Interface;

namespace Api.Services.Implementation;

public class JwtService(ILogger<JwtService> _logger, IOptions<JWTSettings> jwtSettings) : IJwtService
{
    private readonly JWTSettings _jwtSettings = jwtSettings.Value;

    public string GenerateToken(LocalUser user)
    {
        _logger.LogInformation($"Generating JWT token for user: '{user.Username}'");

        var userClaims = new Claim[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = signingCredentials,
            Subject = new ClaimsIdentity(userClaims),
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.ExpiresInDays),
            Issuer = _jwtSettings.Issuer,
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken jwt = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(jwt);
    }
}
