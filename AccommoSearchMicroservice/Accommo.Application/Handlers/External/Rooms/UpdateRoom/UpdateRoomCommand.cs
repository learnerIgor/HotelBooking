using MediatR;

namespace Accommo.Application.Handlers.External.Rooms.UpdateRoom
{
    public class UpdateRoomCommand : IRequest<GetRoomExternalDto>
    {
        public string Id { get; init; }
        public int Floor { get; init; }
        public int Number { get; init; }
        public string RoomTypeId { get; init; }
        public string HotelId { get; init; }
        public string Image { get; init; }
        public int[] Amenities { get; set; }

        public UpdateRoomCommand(string id, UpdateRoomPayload payload)
        {
            Id = id;
            Floor = payload.Floor;
            Number = payload.Number;
            RoomTypeId = payload.RoomTypeId;
            HotelId = payload.HotelId;
            Image = payload.Image;
            Amenities = payload.Amenities;
        }
    }
}
