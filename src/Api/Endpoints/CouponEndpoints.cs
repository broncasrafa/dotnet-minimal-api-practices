using Microsoft.AspNetCore.Mvc;
using Api.Models.DTO.ApiResult;
using Api.Models.DTO.Request;
using Api.Models.DTO.Response;
using Api.Models.Exceptions;
using Api.Services.Interface;
using Api.Validations.Filters;

namespace Api.Endpoints;

public static class CouponEndpoints
{
    public static void ConfigureCouponEndpoints(this WebApplication app)
    {
        var route = app.MapGroup("/api/coupons");

        route.MapGet("/", GetAllCoupons)
            .WithName("GetAllCoupons")
            .Produces<ApiResponse<IEnumerable<CouponResponse>>>(StatusCodes.Status200OK)
            //.RequireAuthorization("AdminOnlyPolicy")
            .RequireAuthorization()
            .WithDescription("Retrieves a list of coupons")
            .WithTags("Coupons")
            .WithOpenApi(options =>
            {
                options.Summary = "Retrieves a list of coupons";
                return options;
            });

        route.MapGet("/{id:int}", GetCoupon)
            .WithName("GetCoupon")
            .Produces<ApiResponse<CouponResponse>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<ProblemDetails>>(StatusCodes.Status404NotFound)
            .Produces<ApiResponse<ProblemDetails>>(StatusCodes.Status400BadRequest)
            .RequireAuthorization()
            .WithDescription("Find a coupon by ID")
            .WithTags("Coupons")
            .WithOpenApi(options =>
            {
                options.Summary = "Find a coupon by a specified ID";
                return options;
            });

        route.MapPost("/", CreateCoupon)
            .WithName("CreateCoupon")
            .Accepts<CouponCreateRequest>("application/json")
            .Produces<ApiResponse<CouponResponse>>(StatusCodes.Status201Created)
            .Produces<ApiResponse<ProblemDetails>>(StatusCodes.Status404NotFound)
            .Produces<ApiResponse<ProblemDetails>>(StatusCodes.Status400BadRequest)
            .AddEndpointFilter<ValidationFilter>()
            .RequireAuthorization()
            .WithDescription("Add a new coupon")
            .WithTags("Coupons")
            .WithOpenApi(options =>
            {
                options.Summary = "Add a new coupon";
                return options;
            });

        route.MapPut("/{id:int}", UpdateCoupon)
            .WithName("UpdateCoupon")
            .Accepts<CouponUpdateRequest>("application/json")
            .Produces<ApiResponse<CouponResponse>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<ProblemDetails>>(StatusCodes.Status404NotFound)
            .Produces<ApiResponse<ProblemDetails>>(StatusCodes.Status400BadRequest)
            .AddEndpointFilter<ValidationFilter>()
            .RequireAuthorization()
            .WithDescription("Updates an existing coupon")
            .WithTags("Coupons")
            .WithOpenApi(options =>
            {
                options.Summary = "Updates an existing coupon by a specified ID";
                return options;
            });

        route.MapDelete("/{id:int}", DeleteCoupon)
            .WithName("DeleteCoupon")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<ProblemDetails>>(StatusCodes.Status404NotFound)
            .Produces<ApiResponse<ProblemDetails>>(StatusCodes.Status400BadRequest)
            .RequireAuthorization()
            .WithDescription("Deletes a coupon by a specified ID")
            .WithTags("Coupons")
            .WithOpenApi(options =>
            {
                options.Summary = "Deletes a coupon";
                return options;
            });
    }


    
    private async static Task<IResult> GetAllCoupons(ILogger<Program> _logger, ICouponService _service)
    {
        _logger.LogInformation("Getting all coupons");

        var response = await _service.GetAllAsync();

        return TypedResults.Ok(ApiResponse<IEnumerable<CouponResponse>>.Success(response));
    }

    private async static Task<IResult> GetCoupon(ILogger<Program> _logger, ICouponService _service, int id)
    {
        _logger.LogInformation($"Getting coupon with id: '{id}'");

        if (id < 1)
        {
            _logger.LogWarning($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Coupon id is required");
        }

        var response = await _service.FindByIdAsync(id);
        return TypedResults.Ok(ApiResponse<CouponResponse>.Success(response));
    }

    private async static Task<IResult> CreateCoupon(ILogger<Program> _logger, ICouponService _service, [FromBody] CouponCreateRequest request)
    {
        _logger.LogInformation($"Creating new coupon with name: '{request.Name}'");

        var response = await _service.InsertAsync(request);
        //return TypedResults.CreatedAtRoute($"GetCoupon", new { id = response.Id }, ApiResponse<CouponResponse>.Success(response));
        return TypedResults.CreatedAtRoute(routeName: "GetCoupon", routeValues: new { id = response.Id }, value: ApiResponse<CouponResponse>.Success(response));
    }

    private async static Task<IResult> UpdateCoupon(ILogger<Program> _logger, ICouponService _service, [FromBody] CouponUpdateRequest request, int id)
    {
        if (id < 1 || request.Id < 1 || (id != request.Id))
        {
            _logger.LogWarning($"Invalid Parameter with value: '{id}', does not match with body id value: '{request.Id}'");
            throw new InvalidParameterBadRequestException($"Coupon id with value: '{id}', does not match with body id value: '{request.Id}'");
        }

        _logger.LogInformation($"Updating coupon with id '{id}'");

        var response = await _service.UpdateAsync(request);
        return TypedResults.Ok(ApiResponse<CouponResponse>.Success(response));
    }

    private async static Task<IResult> DeleteCoupon(ILogger<Program> _logger, ICouponService _service, int id)
    {
        if (id < 1)
        {
            _logger.LogWarning($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Coupon id is required");
        }

        _logger.LogInformation($"Removing coupon with id '{id}'");

        await _service.DeleteAsync(id);

        return TypedResults.Ok(ApiResponse<bool>.Success(true));
    }
}
