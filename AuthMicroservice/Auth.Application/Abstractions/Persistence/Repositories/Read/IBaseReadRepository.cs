namespace Auth.Application.Abstractions.Persistence.Repositories.Read
{
    public interface IBaseReadRepository<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> AsQueryable();

        public IAsyncRead<TEntity> AsAsyncRead();
    }
}
