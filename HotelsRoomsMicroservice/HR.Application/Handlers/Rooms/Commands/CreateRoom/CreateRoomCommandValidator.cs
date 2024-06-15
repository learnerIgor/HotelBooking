using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(f => f.Floor).InclusiveBetween(1, 100);
            RuleFor(n => n.Number).InclusiveBetween(1, 7000);
            RuleFor(r => r.RoomTypeId).NotEmpty().IsGuid();
            RuleFor(h => h.HotelId).IsGuid();
            RuleFor(a => a.Amenities).IsValidAmenities();
            RuleFor(i => i.Image).IsValidImageUrl();
        }
    }
}
