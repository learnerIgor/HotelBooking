using AutoMapper;
using Accommo.Domain;
using Accommo.Application.Abstractions.Mappings;

namespace Accommo.Application.Handlers.External.RoomTypes
{
    public class GetRoomTypeExternalDto : IMapFrom<RoomType>
    {
        public Guid RoomTypeId { get; set; }
        public string Name { get; set; } = default!;
        public decimal BaseCost { get; set; }
        public bool IsActive { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<RoomType, GetRoomTypeExternalDto>();
        }
    }
}
