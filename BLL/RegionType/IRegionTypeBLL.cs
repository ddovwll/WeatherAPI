namespace WeatherAPI.BLL.RegionType;

public interface IRegionTypeBLL
{
    Task AddRegionType(DAL.Models.RegionType model);
    Task UpdateRegionType(DAL.Models.RegionType model);
    Task<DAL.Models.RegionType> GetRegionTypeById(int id);
    Task DeleteRegionType(int id);
    Task<DAL.Models.RegionType> GetRegionTypeByType(string type);
    Task<bool> IsRegionWithTypeExists(int id);
}