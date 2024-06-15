using Booking.Application.ValidatorsExtensions;
using FluentValidation;

namespace Booking.Application.Handlers.Booking.Commands.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(r => r.RoomId).IsGuid();
            RuleFor(r => r.StartDate).IsDateTime().LessThan(r => r.EndDate);
            RuleFor(r => r.EndDate).IsDateTime().GreaterThan(r => r.StartDate);
        }
    }
}
