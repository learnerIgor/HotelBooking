using MediatR;

namespace Accommo.Application.Handlers.External.Rooms.GetRoomById
{
    public class GetRoomByIdQuery : IRequest<GetRoomBookDto>
    {
        public string Id { get; set; } = default!;
    }
}
