namespace HR.Domain
{
    public class City
    {
        public Guid CityId { get; private set; }
        public string Name { get; private set; } = default!;
        public bool IsActive { get; private set; }

        public Guid CountryId { get; private set; }
        public Country Country { get; private set; }
        public IEnumerable<Address> Addresses { get; private set; }

        private City() { }

        public City(string name, Guid countryId, bool isActive)
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
