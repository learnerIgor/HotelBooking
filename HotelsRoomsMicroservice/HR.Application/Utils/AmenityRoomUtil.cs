using HR.Domain.Enums;
using HR.Domain;
using HR.Application.Handlers.Rooms;

namespace HR.Application.Utils
{
    public static class AmenityRoomUtil
    {
        public static List<AmenityRoom> GetAmenitiesRoom(Amenities amenities)
        {
            List<AmenityRoom> amenityRoom = [];
            foreach (var property in amenities.GetType().GetProperties())
            {
                if ((bool)property.GetValue(amenities)!)
                {
                    var amenity = Enum.Parse(typeof(AmenitiesEnum), property.Name);
                    amenityRoom.Add(new AmenityRoom((int)amenity));
                }
            }
            return amenityRoom;
        }
    }
}
