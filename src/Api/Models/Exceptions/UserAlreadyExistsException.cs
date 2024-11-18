using System.Net;
using Api.DTO.Exceptions.Common;

namespace Api.Models.Exceptions;

public class UserAlreadyExistsException : BaseException
{
    public UserAlreadyExistsException(string username, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        : base($"Username '{username}' already exists", statusCode)
    {
    }
}