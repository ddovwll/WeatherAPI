using WeatherAPI.DAL.RegionType;

namespace WeatherAPI.BLL.RegionType;

public class RegionTypeBLL : IRegionTypeBLL
{
    private readonly IRegionTypeDAL regionTypeDal;

    public RegionTypeBLL(IRegionTypeDAL regionTypeDal)
    {
        this.regionTypeDal = regionTypeDal;
    }
    
    public async Task AddRegionType(DAL.Models.RegionType model)
    {
        await regionTypeDal.AddRegionType(model);
    }

    public async Task UpdateRegionType(DAL.Models.RegionType model)
    {
        await regionTypeDal.UpdateRegionType(model);
    }

    public async Task<DAL.Models.RegionType> GetRegionTypeById(int id)
    {
        return await regionTypeDal.GetRegionTypeById(id);
    }

    public async Task DeleteRegionType(int id)
    {
        await regionTypeDal.DeleteRegionType(id);
    }

    public async Task<DAL.Models.RegionType> GetRegionTypeByType(string type)
    {
        return await regionTypeDal.GetRegionTypeByType(type);
    }

    public async Task<bool> IsRegionWithTypeExists(int id)
    {
        return (await regionTypeDal.GetRegionByType(id)).Id != 0;
    }
}