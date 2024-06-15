using FluentValidation;
using Accommo.Application.ValidatorsExtensions;

namespace Accommo.Application.Handlers.External.RoomTypes.DeleteRoomType
{
    public class DeleteRoomTypeCommandValidator : AbstractValidator<DeleteRoomTypeCommand>
    {
        public DeleteRoomTypeCommandValidator()
        {
            RuleFor(i => i.Id).NotEmpty().IsGuid();
        }
    }
}
