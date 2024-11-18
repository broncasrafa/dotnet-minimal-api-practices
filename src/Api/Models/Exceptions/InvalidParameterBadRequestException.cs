using System.Net;
using Api.DTO.Exceptions.Common;

namespace Api.Models.Exceptions;

public class InvalidParameterBadRequestException : BaseException
{
    public InvalidParameterBadRequestException(string message)
        : base(message, HttpStatusCode.BadRequest)
    {
    }
}
