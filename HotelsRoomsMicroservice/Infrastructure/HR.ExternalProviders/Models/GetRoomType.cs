namespace HR.ExternalProviders.Models
{
    public class GetRoomType
    {
        public Guid RoomTypeId { get; set; }
        public string Name { get; set; } = default!;
        public decimal BaseCost { get; set; }
        public bool IsActive { get; set; }
    }
}
