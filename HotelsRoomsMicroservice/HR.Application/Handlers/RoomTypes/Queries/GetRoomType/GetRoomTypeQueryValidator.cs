using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.RoomTypes.Queries.GetRoomType
{
    public class GetRoomTypeQueryValidator : AbstractValidator<GetRoomTypeQuery>
    {
        public GetRoomTypeQueryValidator()
        {
            RuleFor(i => i.Id).NotEmpty().IsGuid();
        }
    }
}
