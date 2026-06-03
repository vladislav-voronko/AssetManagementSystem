using FluentValidation;

namespace Replenishments.Application.Commands.CreateReplenishment
{
    public class CreateReplenishmentValidator : AbstractValidator<CreateReplenishmentCommand>
    {
        public CreateReplenishmentValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be positive.");
            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.");
        }
    }
}
