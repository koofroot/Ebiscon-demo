using EbisconDemo.Api.Models.Validators;
using FluentValidation;

namespace EbisconDemo.Api.Extensions
{
    public static class ValidationExtensions
    {
        public static void AddModelValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegistrationApiModelValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginApiModelValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderApiModelValidator>();
            services.AddValidatorsFromAssemblyContaining<SetRoleApiModelValidator>();
        }
    }
}
