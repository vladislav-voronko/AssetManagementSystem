using FluentValidation;

namespace Assets.Application.Commands.UpdateAsset
{
    public class UpdateAssetValidator : AbstractValidator<UpdateAssetCommand>
    {
        public UpdateAssetValidator()
        {
            RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Type)
                .IsInEnum();
        }
    }
}
