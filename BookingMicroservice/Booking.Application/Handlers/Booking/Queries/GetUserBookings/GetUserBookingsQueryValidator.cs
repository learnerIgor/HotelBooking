using FluentValidation;
using Booking.Application.ValidatorsExtensions;

namespace Booking.Application.Handlers.Booking.Queries.GetUserBookings
{
    public class GetUserBookingsQueryValidator : AbstractValidator<GetUserBookingsQuery>
    {
        public GetUserBookingsQueryValidator()
        {
            RuleFor(i => i.ApplicationUserId).NotEmpty().IsGuid();
        }
    }
}
