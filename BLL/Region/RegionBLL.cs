using WeatherAPI.DAL.Region;

namespace WeatherAPI.BLL.Region;

public class RegionBLL : IRegionBLL
{
    private readonly IRegionDAL regionDal;

    public RegionBLL(IRegionDAL regionDal)
    {
        this.regionDal = regionDal;
    }

    public async Task AddRegion(DAL.Models.Region model, int accountId, int regionTypeId)
    {
        await regionDal.AddRegion(model, accountId, regionTypeId);
    }

    public async Task UpdateRegion(DAL.Models.Region model, int userId, int regionTypeId)
    {
        await regionDal.UpdateRegion(model, userId, regionTypeId);
    }

    public async Task<DAL.Models.Region> GetRegionById(int id)
    {
        return await regionDal.GetRegionById(id);
    }

    public async Task DeleteRegion(int id)
    {
        await regionDal.DeleteRegion(id);
    }

    public async Task<DAL.Models.Region> GetRegionByLatLong(double latitude, double longitude)
    {
        return await regionDal.GetRegionByLatLong(latitude, longitude);
    }

    public async Task<bool> IsRegionParent(string parentName)
    {
        return await regionDal.IsRegionParent(parentName);
    }
}