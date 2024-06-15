using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommand : IImage, IRequest<GetRoomDto>
    {
        public int Floor { get; init; }
        public int Number { get; init; }
        public string RoomTypeId { get; init; } = default!;
        public string HotelId { get; init; } = default!;
        public string Image { get; init; } = default!;

        public Amenities Amenities { get; init; } = default!;
    }
}
