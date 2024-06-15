using Accommo.Application.Abstractions.Mappings;
using Accommo.Domain;
using AutoMapper;

namespace Accommo.Application.Handlers.External.Rooms
{
    public class GetRoomBookDto : IMapFrom<Room>
    {
        public Guid RoomId { get; set; } = default!;
        public int Floor { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }
        public string Image {  get; set; } = default!;
        public RoomTypeBookDto RoomType { get; set; } = default!;
        public HotelBookDto Hotel { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Room, GetRoomBookDto>();
        }
    }
}
