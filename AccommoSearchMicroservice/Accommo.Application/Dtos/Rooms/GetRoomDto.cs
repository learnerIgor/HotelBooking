using AutoMapper;
using Accommo.Application.Abstractions.Mappings;
using Accommo.Domain;

namespace Accommo.Application.Dtos.Rooms
{
    public class GetRoomDto : IMapFrom<Room>
    {
        public Guid RoomId { get; set; } = default!;
        public int Floor { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }
        public Guid RoomTypeId { get; set; }
        public Guid HotelId { get; set; }
        public int[] Amenities { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Room, GetRoomDto>()
                .ForMember(e => e.Amenities, r =>
                    r.MapFrom(u => u.Amenities.Select(s => s.AmenityId).ToArray()));
        }
    }
}
