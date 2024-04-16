namespace WeatherAPI.BLL.Region;

public interface IRegionBLL
{
    Task AddRegion(DAL.Models.Region model, int accountId);
    Task UpdateRegion(DAL.Models.Region model, int userId);
    Task<DAL.Models.Region> GetRegionById(int id);
    Task DeleteRegion(int id);
    Task<DAL.Models.Region> GetRegionByLatLong(double latitude, double longitude);
    Task<bool> IsRegionParent(string parentName);
}