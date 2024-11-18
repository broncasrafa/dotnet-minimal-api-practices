using Api.Models.Entity;

namespace Api.Data.Repository.Interface;

public interface IAuthRepository
{
    bool IsUniqueUser(string username);
    Task<LocalUser> LoginAsync(string username);
    Task<LocalUser> LoginAsync(string username, string hashedPassword);    
    Task<LocalUser> Register(LocalUser localUser);
}
