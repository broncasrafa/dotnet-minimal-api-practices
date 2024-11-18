using System.Net;
using Api.DTO.Exceptions.Common;

namespace Api.Models.Exceptions;

public class SignInErrorException : BaseException
{
    public SignInErrorException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        : base("Incorrect username or password", statusCode)
    {
    }
}
