namespace HR.Domain
{
    public class Address
    {
        public Guid AddressId { get; private set; }
        public string Street { get; private set; }
        public string HouseNumber { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
        public bool IsActive { get; private set; }

        public Guid CityId { get; private set; }
        public City City { get; private set; }

        public Hotel Hotel { get; private set; }

        private Address() { }

        public Address(string street, string houseNumber, decimal latitude, decimal longitude, bool isActive, Guid cityId)
        { 
            if (string.IsNullOrWhiteSpace(street))
            {
                throw new ArgumentException("Street is empty", nameof(street));
            }

            if (street.Length > 50)
            {
                throw new ArgumentException("Street length more than 50", nameof(street));
            }

            if (street.Length < 3)
            {
                throw new ArgumentException("Street length less than 3", nameof(street));
            }

            if (string.IsNullOrWhiteSpace(houseNumber))
            {
                throw new ArgumentException("HouseNumber is empty", nameof(houseNumber));
            }

            if (houseNumber.Length > 10)
            {
                throw new ArgumentException("HouseNumber length more than 10", nameof(houseNumber));
            }

            if (houseNumber.Length < 1)
            {
                throw new ArgumentException("HouseNumber length less than 1", nameof(houseNumber));
            }

            if (latitude < -90 || latitude > 90)
            {
                throw new ArgumentException("Latitude must be between -90 and 90 degrees", nameof(latitude));
            }

            if (longitude < -180 || longitude > 180)
            {
                throw new ArgumentException("Longitude must be between -180 and 180 degrees", nameof(longitude));
            }

            if (cityId == Guid.Empty)
            {
                throw new ArgumentException("CityId must be more than 0", nameof(cityId));
            }

            Street = street;
            HouseNumber = houseNumber;
            Latitude = latitude;
            Longitude = longitude;
            IsActive = isActive;
            CityId = cityId;
        }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
