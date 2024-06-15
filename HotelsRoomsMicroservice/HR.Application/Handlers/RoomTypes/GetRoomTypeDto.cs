using AutoMapper;
using HR.Application.Abstractions.Mappings;
using HR.Domain;

namespace HR.Application.Handlers.RoomTypes
{
    public class GetRoomTypeDto : IMapFrom<RoomType>
    {
        public Guid RoomTypeId { get; set; }
        public string Name { get; set; } = default!;
        public decimal BaseCost { get; set; }
        public bool IsActive { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<RoomType, GetRoomTypeDto>();
        }
    }
}
