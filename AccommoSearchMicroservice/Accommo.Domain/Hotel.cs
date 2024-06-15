namespace Accommo.Domain
{
    public class Hotel
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Rating { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; private set; }

        public Guid AddressId { get; set; }
        public Address Address { get; set; } = default!;
        public IEnumerable<Room> Rooms { get; set; } = new List<Room>();

        private Hotel() { }

        public Hotel(Guid hotelId, string name, Guid addressId, string description, int rating, bool isActive, string image)
        {
            if(hotelId == Guid.Empty)
            {
                throw new ArgumentException("Incorrect hotel Id", nameof(hotelId));
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
        }

        public void UpdateName(string name)
        {
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

            Name = name;
        }

        public void UpdateAddres(Guid addressId)
        {
            AddressId = addressId;
        }

        public void UpdateDescription(string description)
        {
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

            Description = description;
        }

        public void UpdateRating(int rating)
        {
            if (rating > 5)
            {
                throw new ArgumentException("Rating length more than 5", nameof(rating));
            }

            if (rating < 1)
            {
                throw new ArgumentException("Rating length less than 1", nameof(rating));
            }

            Rating = rating;
        }

        public void UpdateIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void UpdateImage(string imageUrl)
        {
            if (!IsUrlTrue(imageUrl))
            {
                throw new ArgumentException("Url of image incorrect", nameof(imageUrl));
            }

            Image = imageUrl;
        }

        private bool IsUrlTrue(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
