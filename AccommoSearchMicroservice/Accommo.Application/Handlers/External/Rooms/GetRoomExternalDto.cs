using Accommo.Domain;
using Accommo.Application.Abstractions.Mappings;
using AutoMapper;

namespace Accommo.Application.Handlers.External.Rooms
{
    public class GetRoomExternalDto : IMapFrom<Room>
    {
        public Guid RoomId { get; set; } = default!;
        public int Floor { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; } = default!;
        public Guid RoomTypeId { get; set; }
        public Guid HotelId { get; set; }
        public int[] Amenities { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Room, GetRoomExternalDto>()
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.Amenities.Select(a => a.AmenityId).ToArray()));

        }
    }
}
