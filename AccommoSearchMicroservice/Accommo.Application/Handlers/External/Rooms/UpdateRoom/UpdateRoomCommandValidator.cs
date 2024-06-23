using Accommo.Application.ValidatorsExtensions;
using FluentValidation;

namespace Accommo.Application.Handlers.External.Rooms.UpdateRoom
{
    public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator()
        {
            RuleFor(f => f.Floor).InclusiveBetween(1, 100);
            RuleFor(n => n.Number).InclusiveBetween(1, 7000);
            RuleFor(r => r.RoomTypeId).NotEmpty().IsGuid();
            RuleFor(h => h.HotelId).IsGuid();
            RuleFor(e => e.Image).IsValidImageUrl().MaximumLength(50);
        }
    }
}
