using Microsoft.EntityFrameworkCore;
using Api.Data.Context;
using Api.Data.Repository.Interface;
using Api.Models.Entity;

namespace Api.Data.Repository.Implementation;

public class AuthRepository(ILogger<AuthRepository> _logger, ApplicationDbContext _context) : IAuthRepository
{
    public bool IsUniqueUser(string username)
    {
        var user = _context.LocalUsers.FirstOrDefault(x => x.Username == username);
        return user is null;
    }

    public async Task<LocalUser> Register(LocalUser localUser)
    {
        _context.LocalUsers.Add(localUser);
        await _context.SaveChangesAsync();
        localUser.Password = string.Empty;
        return localUser;
    }

    public async Task<LocalUser> LoginAsync(string username)
    {
        var user = await _context.LocalUsers.SingleOrDefaultAsync(c => c.Username.ToLower() == username.ToLower());
        return user;
    }

    public async Task<LocalUser> LoginAsync(string username, string hashedPassword)
    {
        var user = await _context.LocalUsers.SingleOrDefaultAsync(c => c.Username.ToLower() == username.ToLower() && c.Password == hashedPassword);
        return user;
    }
}
