using FluentValidation;
using Accommo.Application.ValidatorsExtensions;

namespace Accommo.Application.Handlers.External.Rooms.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(f => f.Floor).InclusiveBetween(1, 100);
            RuleFor(n => n.Number).InclusiveBetween(1, 7000);
            RuleFor(r => r.RoomTypeId).NotEmpty().IsGuid();
            RuleFor(h => h.HotelId).IsGuid();
            RuleFor(e => e.Image).IsValidImageUrl().MaximumLength(50);
        }
    }
}
