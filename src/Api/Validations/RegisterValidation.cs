using Api.Models.DTO.Request;
using Api.Validations.ExtensionsValidators;
using FluentValidation;

namespace Api.Validations;

public class RegisterValidation : AbstractValidator<RegisterRequest>
{
    public RegisterValidation()
    {
        RuleFor(c => c.Username)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Username is required")
            .NotNull().WithMessage("Username is required");

        RuleFor(c => c.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .Length(3, 45).WithMessage($"Name must be between 3 and 100 characters");

        RuleFor(c => c.Password)
            .Cascade(CascadeMode.Stop)
            .PasswordValidations();

        //RuleFor(c => c.ConfirmPassword).Cascade(CascadeMode.Stop)
        //    .PasswordConfirmationValidations()
        //    .Equal(c => c.Password).WithMessage("Confirmation password does not match the chosen password");
    }
}