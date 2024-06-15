namespace Accommo.Application.Caches;

public interface ICleanAccommoCacheService
{
    void ClearAllCaches();
    void ClearListCaches();
}