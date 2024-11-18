using System.Net;
using Api.DTO.Exceptions.Common;

namespace Api.Models.Exceptions;

public class CouponAlreadyExistsException : BaseException
{
    public CouponAlreadyExistsException(string couponName, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        : base($"Coupon with name '{couponName}' already exists", statusCode)
    {
    }
}