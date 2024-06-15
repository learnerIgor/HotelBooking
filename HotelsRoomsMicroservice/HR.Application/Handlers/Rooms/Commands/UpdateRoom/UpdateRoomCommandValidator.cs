using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator()
        {
            RuleFor(f => f.Floor).InclusiveBetween(1, 100);
            RuleFor(n => n.Number).InclusiveBetween(1, 7000);
            RuleFor(r => r.RoomTypeId).NotEmpty().IsGuid();
            RuleFor(h => h.HotelId).IsGuid();
            RuleFor(i => i.Image).IsValidImageUrl();
            RuleFor(a => a.Amenities).IsValidAmenities();
        }
    }
}
