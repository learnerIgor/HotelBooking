using AutoMapper;
using Booking.Application.Abstractions.Mappings;
using Booking.Domain;

namespace Booking.Application.Handlers.Booking
{
    public class PaymentDto : IMapFrom<Payment>
    {
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Payment, PaymentDto>();
        }
    }
}
