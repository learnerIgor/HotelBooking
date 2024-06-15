namespace Accommo.Application.Dtos
{
    public class BaseListDto<T> where T : class, new()
    {
        public T[] Items { get; init; } = default!;

        public int TotalCount { get; init; }
    }
}
