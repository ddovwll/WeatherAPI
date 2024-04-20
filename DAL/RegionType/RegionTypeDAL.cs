using Microsoft.EntityFrameworkCore;

namespace WeatherAPI.DAL.RegionType;

public class RegionTypeDAL : IRegionTypeDAL
{
    public async Task AddRegionType(Models.RegionType model)
    {
        await using var context = new Context();
        await context.RegionTypes.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public async Task UpdateRegionType(Models.RegionType model)
    {
        await using var context = new Context();
        await Task.Run(() =>
        {
            context.RegionTypes.Update(model);
        });
        await context.SaveChangesAsync();
    }

    public async Task<Models.RegionType> GetRegionTypeById(int id)
    {
        await using var context = new Context();
        return await context.RegionTypes.FindAsync(id) ?? new Models.RegionType();
    }

    public async Task DeleteRegionType(int id)
    {
        await using var context = new Context();
        var model = await GetRegionTypeById(id);
        await Task.Run(() =>
        {
            context.RegionTypes.Remove(model);
        });
        await context.SaveChangesAsync();
    }

    public async Task<Models.RegionType> GetRegionTypeByType(string type)
    {
        await using var context = new Context();
        return await context.RegionTypes.FirstOrDefaultAsync(rt => rt.Type == type) 
               ?? new Models.RegionType();
    }

    public async Task<Models.Region> GetRegionByType(int id)
    {
        await using var context = new Context();
        return await context.Regions.FirstOrDefaultAsync(r => r.RegionType == id) 
               ?? new Models.Region();
    }
}