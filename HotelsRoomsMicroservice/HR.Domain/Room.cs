namespace HR.Domain
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

        public IEnumerable<AmenityRoom> Amenities { get; private set; }

        private Room() { }

        public Room(int floor, int number, Guid roomTypeId, bool isActive, Guid hotelId, AmenityRoom[] amenities, string image)
        {
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

            if(!IsUrlTrue(image))
            {
                throw new ArgumentException("Url of image incorrect", nameof(image));
            }

            Floor = floor;
            Number = number;
            IsActive = isActive;
            RoomTypeId = roomTypeId;
            HotelId = hotelId;
            Amenities = amenities;
            Image = image;
        }

        public void UpdateFloor(int floor)
        {
            if (floor > 100)
            {
                throw new ArgumentException("Floor more than 100", nameof(floor));
            }

            if (floor < 1)
            {
                throw new ArgumentException("Floor less than 1", nameof(floor));
            }

            Floor = floor;
        }

        public void UpdateRoomType(Guid roomTypeId)
        {
            if (roomTypeId == Guid.Empty)
            {
                throw new ArgumentException("RoomTypeId less than 1", nameof(roomTypeId));
            }

            RoomTypeId = roomTypeId;
        }

        public void UpdateHotel(Guid hotelId)
        {
            HotelId = hotelId;
        }

        public void UpdateNumber(int number)
        {
            if (number > 7000)
            {
                throw new ArgumentException("Number more than 7000", nameof(number));
            }

            if (number < 1)
            {
                throw new ArgumentException("Number less than 1", nameof(number));
            }

            Number = number;
        }

        public void UpdateAmenities(List<AmenityRoom> amenities)
        {
            if (amenities is null)
            {
                throw new ArgumentException("Amenities must be more than null", nameof(amenities));
            }

            Amenities = amenities;
        }

        public void UpdateImage(string imageUrl)
        {
            if (!IsUrlTrue(imageUrl))
            {
                throw new ArgumentException("Url of image incorrect", nameof(imageUrl));
            }

            Image = imageUrl;
        }

        public void UpdateIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        private bool IsUrlTrue(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
