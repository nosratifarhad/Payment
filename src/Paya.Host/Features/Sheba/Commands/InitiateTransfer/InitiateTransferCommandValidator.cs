using FluentValidation;

namespace Paya.Host.Features.Sheba.Commands.InitiateTransfer
{
    public class InitiateTransferCommandValidator : AbstractValidator<InitiateTransferCommand>
    {
        public InitiateTransferCommandValidator()
        {
            RuleFor(p => p.FromShebaNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} must not be empty.")
                .NotNull().WithMessage("{PropertyName} must not be null.")
                .Must(StartWithIR).WithMessage("{PropertyName} must not be empty.")
                .Must(BeValidIbanLength).WithMessage("{PropertyName} must not be empty.")
                .MaximumLength(24).WithMessage("{PropertyName} must not exceed 150 characters.");

            RuleFor(p => p.ToShebaNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} must not be empty.")
                .NotNull().WithMessage("{PropertyName} must not be null.")
                .Must(StartWithIR).WithMessage("{PropertyName} must not be empty.")
                .Must(BeValidIbanLength).WithMessage("{PropertyName} must not be empty.")
                .MaximumLength(24).WithMessage("{PropertyName} must not exceed 150 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("{PropertyName} Must Be Greater Than 0.");
        }

        private bool StartWithIR(string iban)
        {
            return iban.StartsWith("IR", StringComparison.OrdinalIgnoreCase);
        }

        private bool BeValidIbanLength(string iban)
        {
            var digitsPart = iban.Substring(2);

            return digitsPart.All(char.IsDigit);
        }
    }
}
