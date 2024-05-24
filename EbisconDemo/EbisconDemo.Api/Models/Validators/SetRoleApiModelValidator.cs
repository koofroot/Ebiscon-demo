using FluentValidation;

namespace EbisconDemo.Api.Models.Validators
{
    public class SetRoleApiModelValidator : AbstractValidator<SetRoleApiModel>
    {
        public SetRoleApiModelValidator()
        {
            RuleFor(x => x.Role)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.UserEmail)
                .EmailAddress()
                .NotEmpty()
                .NotNull();
        }
    }
}
