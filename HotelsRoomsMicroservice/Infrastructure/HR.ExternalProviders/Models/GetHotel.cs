using HR.Application.Abstractions.Mappings;
using HR.Domain;

namespace HR.ExternalProviders.Models
{
    public class GetHotel : IMapFrom<Hotel>
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string IBAN { get; set; } = default!;
        public int Rating { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; } = default!;

        public Guid AddressId { get; set; }
        public GetAddress Address { get; set; } = default!;
    }
}
