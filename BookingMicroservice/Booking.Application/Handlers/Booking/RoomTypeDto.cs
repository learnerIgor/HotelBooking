using AutoMapper;
using Booking.Application.Abstractions.Mappings;
using Booking.Domain;

namespace Booking.Application.Handlers.Booking
{
    public class RoomTypeDto : IMapFrom<RoomType>
    {
        public string Name { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<RoomType, RoomTypeDto>();
        }
    }
}
