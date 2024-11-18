using Api.Models.DTO.Request;
using FluentValidation;

namespace Api.Validations;

public class LoginValidation : AbstractValidator<LoginRequest>
{
    public LoginValidation()
    {
        RuleFor(c => c.Username)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Username is required")
            .NotNull().WithMessage("Username is required");

        RuleFor(c => c.Password)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Password is required")
            .NotEmpty().WithMessage("Password is required");
    }
}
