using FluentValidation;
using Booking.Application.ValidatorsExtensions;

namespace Booking.Application.Handlers.Booking.Queries.GetBookingsCount
{
    internal class GetBookingsCountQueryValidator : AbstractValidator<GetBookingsCountQuery>
    {
        public GetBookingsCountQueryValidator()
        {
            RuleFor(d => d.StartDate).IsDateTime().LessThan(d => d.EndDate);
            RuleFor(d => d.EndDate).IsDateTime().GreaterThan(d => d.StartDate);
        }

    }
}