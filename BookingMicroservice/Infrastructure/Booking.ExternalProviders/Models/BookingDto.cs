namespace Booking.ExternalProviders.Models
{
    public class BookingDto
    {
        public Guid ReservationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsActive {  get; set; }
        public Guid ApplicationUserId { get; set; }
        public Guid RoomId { get; set; }
    }
}
