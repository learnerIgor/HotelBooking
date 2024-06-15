using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Location.Countries.Queries.GetCountry
{
    public class GetCountryQueryValidator : AbstractValidator<GetCountryQuery>
    {
        public GetCountryQueryValidator()
        {
            RuleFor(i => i.Id).IsGuid().NotEmpty();
        }
    }
}
