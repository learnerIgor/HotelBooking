using Accommo.Application.Abstractions.Mappings;
using Accommo.Domain;
using AutoMapper;

namespace Accommo.Application.Handlers.External.Rooms
{
    public class HotelBookDto : IMapFrom<Hotel>
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Rating { get; set; }
        public bool IsActive {  get; set; }
        public string IBAN {  get; set; } = default!;
        public string Image {  get; set; } = default!;
        public AddressBookDto Address { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Hotel, HotelBookDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .AfterMap((src, dest) =>
                {
                    if (dest.Address != null)
                    {
                        dest.Address.City = new CityBookDto
                        {
                            CityId = dest.Address.City.CityId,
                            Name = dest.Address.City.Name,
                            Country = new CountryBookDto
                            {
                                CountryId = dest.Address.City.Country.CountryId,
                                Name = dest.Address.City.Country.Name
                            }
                        };
                    }
                });

            profile.CreateMap<Address, AddressBookDto>();
            profile.CreateMap<City, CityBookDto>();
            profile.CreateMap<Country, CountryBookDto>();
        }
    }
}
