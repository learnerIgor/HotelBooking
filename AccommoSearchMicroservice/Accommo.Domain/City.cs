namespace Accommo.Domain
{
    public class City
    {
        public Guid CityId { get; set; }
        public string Name { get; set; } = default!;
        public bool IsActive { get; private set; }

        public Guid CountryId { get; set; }
        public Country Country { get; set; } = default!;
        public IEnumerable<Address> Addresses { get; set; } = default!;

        private City() { }

        public City(Guid cityId, string name, Guid countryId, bool isActive)
        {
            if (cityId == Guid.Empty)
            {
                throw new ArgumentException("Incorrect city Id", nameof(cityId));
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

        public void UpdateIsActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
