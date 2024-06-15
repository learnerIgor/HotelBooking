using Accommo.Application.ValidatorsExtensions;
using FluentValidation;

namespace Accommo.Application.Handlers.External.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidator()
        {
            RuleFor(r => r.ReservationId).IsGuid();
            RuleFor(r => r.StartDate).IsDateTime().LessThan(r => r.EndDate);
            RuleFor(r => r.EndDate).IsDateTime().GreaterThan(r => r.StartDate);
        }
    }
}
