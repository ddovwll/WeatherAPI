namespace WeatherAPI.DAL.Region;

public interface IRegionDAL
{
    Task AddRegion(Models.Region model, int accountId, int regionType);
    Task UpdateRegion(Models.Region model, int userId, int regionType);
    Task<Models.Region> GetRegionById(int id);
    Task DeleteRegion(int id);
    Task<Models.Region> GetRegionByLatLong(double latitude, double longitude);
    Task<bool> IsRegionParent(string parentName);
}