namespace Booking.Application.Utils
{
    public static class AmountUtil
    {
        public static decimal CalculateAmount(DateTime start, DateTime end, decimal costRoom)
        {
            var countDays = end.Subtract(start).TotalDays + 1;
            var totalAmmount = (decimal)countDays * costRoom;

            return totalAmmount;
        }
    }
}