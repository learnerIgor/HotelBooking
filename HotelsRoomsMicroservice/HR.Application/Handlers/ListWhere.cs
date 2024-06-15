using HR.Domain;
using System.Linq.Expressions;

namespace HR.Application.Handlers
{
    internal static class ListWhere
    {
        public static Expression<Func<Hotel, bool>> WhereHotels(ListFilter filter)
        {
            var freeText = filter.FreeText?.Trim();
            return hotel => (freeText == null || hotel.Name.Contains(freeText)) && hotel.IsActive;
        }

        public static Expression<Func<RoomType, bool>> WhereRoomTypes(ListFilter filter)
        {
            var freeText = filter.FreeText?.Trim();
            return roomType => (freeText == null || roomType.Name.Contains(freeText)) && roomType.IsActive;
        }
    }
}