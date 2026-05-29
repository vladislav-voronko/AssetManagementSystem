using FluentValidation;

namespace Assets.Application.Commands.CreateAsset
{
    public class CreateAssetValidator : AbstractValidator<CreateAssetCommand>
    {
        public CreateAssetValidator()
        {
            RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Currency)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}
