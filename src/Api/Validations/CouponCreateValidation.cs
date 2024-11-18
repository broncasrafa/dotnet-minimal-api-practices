using Api.Models.DTO.Request;
using Api.Services.Interface;
using FluentValidation;

namespace Api.Validations;

public class CouponCreateValidation : AbstractValidator<CouponCreateRequest>
{
    private readonly ICouponService _service;
    public CouponCreateValidation(ICouponService service)
    {
        _service = service;

        RuleFor(model => model.Name).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Coupon name is required")
            .NotNull().WithMessage("Coupon name is required")
            .Length(3, 15).WithMessage($"Coupon name must be between 3 and 15 characters")
            .MustAsync(CheckIfCouponNameAlreadyExistsAsync).WithMessage("Coupon name already exists");

        RuleFor(model => model.Percent).Cascade(CascadeMode.Stop)
            .InclusiveBetween(1, 100).WithMessage("Coupon percentual must be 1 between 100");
    }

    private async Task<bool> CheckIfCouponNameAlreadyExistsAsync(string couponName, CancellationToken cancellationToken)
    {
        var coupon = await _service.FindByAsync(c => c.Name == couponName);
        return coupon is null;
    }
}