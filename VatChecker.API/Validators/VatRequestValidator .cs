using FluentValidation;
using VatChecker.Models.DTO;

namespace VatChecker.API.Validators;

public class VatRequestValidator : AbstractValidator<VatRequest>
{
    public VatRequestValidator()
    {
        // Country code must not be empty and must be exactly 2 characters
        RuleFor(x => x.CountryCode)
            .NotEmpty().WithMessage("CountryCode is required")
            .Length(2).WithMessage("CountryCode must be exactly 2 characters");

        // VAT number must not be empty, must be numeric, and have proper length
        RuleFor(x => x.VatNumber)
            .NotEmpty().WithMessage("VatNumber is required")
            .Matches(@"^\d+$").WithMessage("VatNumber must contain only numbers")
            .MinimumLength(8).WithMessage("VatNumber is too short")
            .MaximumLength(12).WithMessage("VatNumber is too long");
    }
}