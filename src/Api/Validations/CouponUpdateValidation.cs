using Api.Models.DTO.Request;
using Api.Services.Interface;
using FluentValidation;

namespace Api.Validations;

public class CouponUpdateValidation : AbstractValidator<CouponUpdateRequest>
{
    private readonly ICouponService _service;
    public CouponUpdateValidation(ICouponService service)
    {
        _service = service;

        RuleFor(model => model.Id).NotEmpty().GreaterThan(0).WithMessage("Coupon id must be greater than 0");

        RuleFor(model => model.Name)
            .NotEmpty().WithMessage("Coupon name is required")
            .NotNull().WithMessage("Coupon name is required")
            .MustAsync(ValidCouponNameExistsAsync).WithMessage("Coupon name already exists");

        RuleFor(model => model.Percent)
            .InclusiveBetween(1, 100).WithMessage("Coupon percentual must be 1 between 100");
    }

    private async Task<bool> ValidCouponNameExistsAsync(CouponUpdateRequest request, string couponName, CancellationToken cancellationToken)
    {
        var coupon = await _service.FindByAsync(c => c.Name == couponName);
        if (coupon is null) return true;
        return coupon.Id == request.Id;
    }
}