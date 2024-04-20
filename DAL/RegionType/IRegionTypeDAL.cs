namespace WeatherAPI.DAL.RegionType;

public interface IRegionTypeDAL
{
    Task AddRegionType(Models.RegionType model);
    Task UpdateRegionType(Models.RegionType model);
    Task<Models.RegionType> GetRegionTypeById(int id);
    Task DeleteRegionType(int id);
    Task<Models.RegionType> GetRegionTypeByType(string type);
    Task<Models.Region> GetRegionByType(int id);
}