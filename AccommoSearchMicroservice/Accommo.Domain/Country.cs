namespace Accommo.Domain
{
    public class Country
    {
        public Guid CountryId { get; private set; }
        public string Name { get; private set; } = default!;
        public bool IsActive { get; private set; }

        public IEnumerable<City> Cities { get; private set; } = default!;

        private Country() { }

        public Country(Guid countryId, string name, bool isActive)
        {
            if (countryId == Guid.Empty)
            {
                throw new ArgumentException("Incorrect country Id", nameof(countryId));
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

            CountryId = countryId;
            Name = name;
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
