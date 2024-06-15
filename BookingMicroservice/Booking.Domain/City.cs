namespace Booking.Domain
{
    public class City
    {
        public Guid CityId { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        public Guid CountryId { get; private set; }
        public Country Country { get; private set; }
        public IEnumerable<Address> Addresses { get; private set; }

        public City(Guid cityId, string name, Guid countryId, bool isActive)
        {
            if(cityId == Guid.Empty)
            {
                throw new ArgumentException("CityId is empty", nameof(cityId));
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

            CityId = cityId;
            Name = name;
            CountryId = countryId;
            IsActive = isActive;
        }

        private City() { }
    }
}
