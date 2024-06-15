using FluentValidation;
using Booking.Application.ValidatorsExtensions;

namespace Booking.Application.Handlers.Booking.Queries.GetBooking
{
    public class GetBookingQueryValidator : AbstractValidator<GetBookingQuery>
    {
        public GetBookingQueryValidator()
        {
            RuleFor(i => i.ReservationId).NotEmpty().IsGuid();
        }
    }
}
