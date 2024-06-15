using AutoMapper;
using Accommo.Application.Abstractions.Mappings;
using Accommo.Domain;

namespace Accommo.Application.Dtos.Hotels
{
    public class GetHotelDto : IMapFrom<Hotel>
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Rating { get; set; }
        //public Guid[] Rooms { get; set; } = default!;
        public AddressDto Address { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Hotel, GetHotelDto>()
                //.ForMember(e => e.Rooms, r => r.MapFrom(u => u.Rooms.Select(s => s.RoomId).ToArray()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .AfterMap((src, dest) =>
                {
                    if (dest.Address != null)
                    {
                        dest.Address.City = new CityDto
                        {
                            Name = dest.Address.City.Name,
                            Country = new CountryDto
                            {
                                Name = dest.Address.City.Country.Name
                            }
                        };
                    }
                });

            profile.CreateMap<Address, AddressDto>();
            profile.CreateMap<City, CityDto>();
            profile.CreateMap<Country, CountryDto>();
        }

        //public void CreateMap(Profile profile)
        //{
        //    profile.CreateMap<Hotel, GetHotelDto>()
        //        .ForMember(e => e.Rooms, r =>
        //            r.MapFrom(u => u.Rooms.Select(s => s.RoomId).ToArray()));
        //}
    }
}
