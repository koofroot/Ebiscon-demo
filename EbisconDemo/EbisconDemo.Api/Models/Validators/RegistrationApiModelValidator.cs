using FluentValidation;

namespace EbisconDemo.Api.Models.Validators
{
    public class RegistrationApiModelValidator : AbstractValidator<RegistrationApiModel>
    {
        public RegistrationApiModelValidator() 
        { 
            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("Invalid email address.");
            RuleFor(x => x.Password).Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}$")
                .WithMessage("Password must contain Upper and lower case letters, have a number, symbol and be between 8 and 16 characters long.");
            RuleFor(x => x.PasswordRepeat).Equal(x => x.Password)
                .WithMessage("Passwords do not match");
            RuleFor(x => x.FirstName).NotNull().NotEmpty()
                .WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotNull().NotEmpty()
                .WithMessage("Last name is required.");
        }
    }
}
