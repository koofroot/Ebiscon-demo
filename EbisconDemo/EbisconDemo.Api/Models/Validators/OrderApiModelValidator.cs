using FluentValidation;

namespace EbisconDemo.Api.Models.Validators
{
    public class OrderApiModelValidator : AbstractValidator<OrderApiModel>
    {
        public OrderApiModelValidator()
        {
            RuleFor(x => x.ProductId).NotNull().GreaterThan(0)
               .WithMessage("Invalid product id.");
            RuleFor(x => x.Count).NotNull().GreaterThan(0)
                .WithMessage("Count must be greater than 0.");
        }
    }
}
