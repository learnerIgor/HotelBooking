namespace Accommo.Domain
{
    public class Room
    {
        public Guid RoomId { get; set; } = default!;
        public int Floor { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; private set; }

        public Guid RoomTypeId {  get; set; }
        public RoomType RoomType { get; set; } = default!;

        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; } = default!;

        public IEnumerable<AmenityRoom> Amenities { get; set; } = default!;

        public IEnumerable<Reservation> Reservations { get; set; } = default!;

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
                throw new ArgumentException("RoomTypeId less than 1", nameof(roomTypeId));
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


        public Room(Guid roomId, int floor, int number, Guid roomTypeId, bool isActive, Guid hotelId, int[] amenityIds, string image)
        {
            if (roomId == Guid.Empty)
            {
                throw new ArgumentException("Incorrect format Id for room", nameof(roomId));
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
                throw new ArgumentException("RoomTypeId less than 1", nameof(roomTypeId));
            }

            if (!IsUrlTrue(image))
            {
                throw new ArgumentException("Url of image incorrect", nameof(image));
            }
            if (amenityIds.Length == 0)
            {
                throw new ArgumentException("Amenities is null", nameof(amenityIds));
            }

            RoomId = roomId;
            Floor = floor;
            Number = number;
            IsActive = isActive;
            RoomTypeId = roomTypeId;
            HotelId = hotelId;
            Amenities = amenityIds.Select(id => new AmenityRoom(id)).ToList();
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

        public void UpdateAmenities(int[] amenityIds)
        {
            if (amenityIds.Length == 0)
            {
                throw new ArgumentException("Amenities is null", nameof(amenityIds));
            }

            Amenities = amenityIds.Select(id => new AmenityRoom(id)).ToList();
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
