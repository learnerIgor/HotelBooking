using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.RoomTypes.Commands.DeleteRoomType
{
    public class DeleteRoomTypeCommandValidator : AbstractValidator<DeleteRoomTypeCommand>
    {
        public DeleteRoomTypeCommandValidator()
        {
            RuleFor(i => i.Id).NotEmpty().IsGuid();
        }
    }
}
