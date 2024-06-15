namespace Booking.Domain
{
    public class Room
    {
        public Guid RoomId { get; private set; }
        public int Floor { get; private set; }
        public int Number { get; private set; }
        public bool IsActive { get; private set; }
        public string Image { get; private set; }


        public Guid RoomTypeId { get; private set; }
        public RoomType RoomType { get; private set; }

        public Guid HotelId { get; private set; }
        public Hotel Hotel { get; private set; }

        public IEnumerable<Reservation> Reservations { get; private set; }

        public Room(Guid roomId, int floor, int number, Guid roomTypeId, bool isActive, Guid hotelId, string image)
        {
            if(roomId == Guid.Empty)
            {
                throw new ArgumentException("RoomId is empty", nameof(roomId));
            }
            if (floor > 100)
            {
                throw new ArgumentException("Floor more than 100", nameof(floor));
            }

            if (floor < 1)
            {
                throw new ArgumentException("Floor less than 1", nameof(floor));
            }

            if (number > 7000)
            {
                throw new ArgumentException("Number more than 7000", nameof(number));
            }

            if (number < 1)
            {
                throw new ArgumentException("Number less than 1", nameof(number));
            }

            if (roomTypeId == Guid.Empty)
            {
                throw new ArgumentException("Incorrect RoomTypeId", nameof(roomTypeId));
            }

            RoomId = roomId;
            Floor = floor;
            Number = number;
            IsActive = isActive;
            RoomTypeId = roomTypeId;
            HotelId = hotelId;
            Image = image;
        }

        private Room() { }
    }
}
