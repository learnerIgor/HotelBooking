namespace Users.Application.Dtos
{
    public interface IBasePaginationFilter
    {
        public int? Limit { get; init; }

        public int? Offset { get; init; }
    }
}
