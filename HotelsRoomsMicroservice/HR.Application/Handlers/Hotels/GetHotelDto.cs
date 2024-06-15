using AutoMapper;
using HR.Application.Abstractions.Mappings;
using HR.Domain;

namespace HR.Application.Handlers.Hotels
{
    public class GetHotelDto : IMapFrom<Hotel>
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string IBAN { get; set; } = default!;
        public int Rating { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; } = default!;
        public AddressDto Address { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Hotel, GetHotelDto>()
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
    }
}
