namespace Accommo.Domain
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsActive { get; set; }
        public Guid RoomId { get; set; } = default!;
        public Room Room { get; set; } = default!;

        public Reservation(Guid reservationId, DateTime checkInDate, DateTime checkOutDate, bool isActive, Guid roomId)
        {
            if(reservationId == Guid.Empty)
            {
                throw new ArgumentNullException("Incorrect reservation Id", nameof(reservationId));
            }
            if (checkInDate < DateTime.MinValue)
            {
                throw new ArgumentNullException("CheckInDate cannot be in the past", nameof(checkInDate));
            }
            if (checkOutDate < DateTime.MinValue)
            {
                throw new ArgumentNullException("CheckOutDate cannot be in the past", nameof(checkOutDate));
            }
            ReservationId = reservationId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            IsActive = isActive;
            RoomId = roomId;
        }

        public void UpdateIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void UpdateCheckInDate(DateTime checkInDate)
        {
            if (checkInDate < DateTime.MinValue)
            {
                throw new ArgumentNullException("CheckInDate cannot be in the past", nameof(checkInDate));
            }
            CheckInDate = checkInDate;
        }

        public void UpdateCheckOutDate(DateTime checkOutDate)
        {
            if (checkOutDate < DateTime.MinValue)
            {
                throw new ArgumentNullException("CheckOutDate cannot be in the past", nameof(checkOutDate));
            }
            CheckOutDate = checkOutDate;
        }

        private Reservation() { }
    }
}
