using FluentValidation;
using Booking.Application.ValidatorsExtensions;

namespace Booking.Application.Handlers.Booking.Commands.DeleteBooking
{
    public class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
    {
        public DeleteBookingCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().IsGuid();
        }
    }
}
