using FluentValidation;

namespace EbisconDemo.Api.Models.Validators
{
    public class LoginApiModelValidator : AbstractValidator<LoginApiModel>
    {
        public LoginApiModelValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
