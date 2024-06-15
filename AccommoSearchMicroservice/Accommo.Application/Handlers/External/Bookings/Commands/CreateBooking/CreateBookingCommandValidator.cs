using Accommo.Application.ValidatorsExtensions;
using FluentValidation;

namespace Accommo.Application.Handlers.External.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(r => r.ReservationId).IsGuid();
            RuleFor(r => r.CheckInDate).IsDateTime().LessThan(r => r.CheckOutDate);
            RuleFor(r => r.CheckOutDate).IsDateTime().GreaterThan(r => r.CheckInDate);
            RuleFor(r => r.RoomId).IsGuid();
        }
    }
}
