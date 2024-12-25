using FluentValidation;
using loc_api_crud.Models;
namespace loc_api_crud.ModelValidator
{
    public class CountryValidator:AbstractValidator<CountryModel>
    {
        public CountryValidator()
        {
            RuleFor(c=>c.CountryName).NotEmpty().NotNull().WithMessage("Country name must not be empty or null");
            RuleFor(c=>c.CountryCode).NotEmpty().NotNull().WithMessage("Country code must not be empty or null");
        }
    }
}
