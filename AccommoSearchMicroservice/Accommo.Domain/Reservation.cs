namespace Accommo.Domain
{
    public class Reservation
    {
        public Guid ReservationId { get; private set; }
        public DateTime CheckInDate { get; private set; }
        public DateTime CheckOutDate { get; private set; }
        public bool IsActive { get; private set; }
        public Guid RoomId { get; private set; } = default!;
        public Room Room { get; private set; } = default!;

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
