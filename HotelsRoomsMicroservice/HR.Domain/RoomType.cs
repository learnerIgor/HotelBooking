namespace HR.Domain
{
    public class RoomType
    {
        public Guid RoomTypeId { get; private set; }
        public string Name { get; private set; }
        public decimal BaseCost { get; private set; }
        public bool IsActive { get; private set; }

        public IEnumerable<Room> Rooms { get; private set; } = new List<Room>();

        public RoomType(string name, decimal baseCost, bool isActive)
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

            if(baseCost == 0)
            {
                throw new ArgumentException("BaseCost must be more than 0", nameof(baseCost));
            }

            if (baseCost < 0)
            {
                throw new ArgumentException("BaseCost must be more than 0", nameof(baseCost));
            }

            Name = name;
            BaseCost = baseCost;
            IsActive = isActive;
        }

        public void UpdateIsActive(bool isActive)
        {
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

        public void UpdateBaseCost(decimal baseCost)
        { 
            if (baseCost == 0)
            {
                throw new ArgumentException("BaseCost must be more than 0", nameof(baseCost));
            }

            if (baseCost < 0)
            {
                throw new ArgumentException("BaseCost must be more than 0", nameof(baseCost));
            }

            BaseCost = baseCost;
        }

        private RoomType() { }
    }
}
