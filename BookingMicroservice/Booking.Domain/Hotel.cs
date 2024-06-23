namespace Booking.Domain
{
    public class Hotel
    {
        public Guid HotelId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Rating { get; private set; }
        public bool IsActive { get; private set; }
        public string IBAN { get; private set; }
        public string Image { get; private set; }

        public Guid AddressId { get; private set; }
        public Address Address { get; private set; }
        public IEnumerable<Room> Rooms { get; private set; } = new List<Room>();

        public Hotel(Guid hotelId, string name, Guid addressId, string description, int rating, bool isActive, string image, string iban)
        {
            if(hotelId == Guid.Empty)
            {
                throw new ArgumentException("HotelId is empty", nameof(hotelId));
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is empty", nameof(name));
            }

            if (name.Length > 50)
            {
                throw new ArgumentException("Name length more than 50", nameof(name));
            }

            if (name.Length < 3)
            {
                throw new ArgumentException("Name length less than 3", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Description is empty", nameof(description));
            }

            if (description.Length > 500)
            {
                throw new ArgumentException("Description length more than 500", nameof(description));
            }

            if (description.Length < 10)
            {
                throw new ArgumentException("Description length less than 10", nameof(description));
            }

            if (rating > 5)
            {
                throw new ArgumentException("Rating length more than 5", nameof(rating));
            }

            if (rating < 1)
            {
                throw new ArgumentException("Rating length less than 1", nameof(rating));
            }

            if (!IsUrlTrue(image))
            {
                throw new ArgumentException("Url of image incorrect", nameof(image));
            }

            HotelId = hotelId;
            Name = name;
            AddressId = addressId;
            Description = description;
            Rating = rating;
            IsActive = isActive;
            Image = image;
            IBAN = iban;
        }

        private bool IsUrlTrue(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private Hotel() { }
    }
}
