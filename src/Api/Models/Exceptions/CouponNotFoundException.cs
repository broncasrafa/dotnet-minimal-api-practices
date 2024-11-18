using System.Net;
using Api.DTO.Exceptions.Common;

namespace Api.Models.Exceptions;

public class CouponNotFoundException : BaseException
{
    public CouponNotFoundException(int id, HttpStatusCode statusCode = HttpStatusCode.NotFound) 
        : base($"Coupon with ID '{id}' not found", statusCode)
    {
    }
}