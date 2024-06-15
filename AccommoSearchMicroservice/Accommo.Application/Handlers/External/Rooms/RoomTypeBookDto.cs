using Accommo.Application.Abstractions.Mappings;
using Accommo.Domain;
using AutoMapper;

namespace Accommo.Application.Handlers.External.Rooms
{
    public class RoomTypeBookDto : IMapFrom<RoomType>
    {
        public Guid RoomTypeId { get; set; }
        public string Name { get; set; } = default!;
        public decimal BaseCost { get; set; }
        public bool IsActive { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<RoomType, RoomTypeBookDto>();
        }
    }
}
