using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Location.Countries.Queries.GetCountries
{
    public class GetCountriesQueryValidator : AbstractValidator<GetCountriesQuery>
    {
        public GetCountriesQueryValidator()
        {
            RuleFor(e => e).IsValidListFilter();
            RuleFor(e => e).IsValidPaginationFilter();
        }
    }
}
