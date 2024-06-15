namespace Booking.Domain
{
    public class Reservation
    {
        public Guid ReservationId { get; private set; }
        public DateTime CheckInDate { get; private set; }
        public DateTime CheckOutDate { get; private set; }
        public bool IsActive { get; private set; }

        public Guid ApplicationUserId { get; private set; }
        public ApplicationUser ApplicationUser { get; private set; }

        public Guid PaymentId { get; private set; }
        public Payment Payment { get; private set; }

        public Guid RoomId { get; private set; }
        public Room Room { get; private set; }

        private Reservation() { }

        public Reservation(DateTime checkInDate, DateTime checkOutDate, bool isActive, Guid applicationUserId, Guid roomId, Guid paymentId)
        {
            if (checkInDate < DateTime.MinValue)
            {
                throw new ArgumentNullException("CheckInDate cannot be in the past", nameof(checkInDate));
            }
            if (checkOutDate < DateTime.MinValue)
            {
                throw new ArgumentNullException("CheckOutDate cannot be in the past", nameof(checkOutDate));
            }
            if (paymentId == Guid.Empty)
            {
                throw new ArgumentNullException("PaymentId cannot be null", nameof(paymentId));
            }
            if(applicationUserId == Guid.Empty)
            {
                throw new ArgumentNullException("ApplicationUserId cannot be null", nameof(applicationUserId));
            }
            if (roomId == Guid.Empty)
            {
                throw new ArgumentNullException("RoomId cannot be null", nameof(roomId));
            }
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            IsActive = isActive;
            ApplicationUserId = applicationUserId;
            RoomId = roomId;
            PaymentId = paymentId;
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
    }
}
