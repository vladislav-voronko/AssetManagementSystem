using FluentValidation;

namespace Trades.Application.Commands.CreateTrade
{
    public class CreateTradeValidator : AbstractValidator<CreateTradeCommand>
    {
        public CreateTradeValidator()
        {
            RuleFor(x => x.AssetId)
                .NotEmpty()
                .WithMessage("AssetId is required.");
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be positive.");
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be positive.");
            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Invalid trade type.");
            RuleFor(x => x.Currency)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}
