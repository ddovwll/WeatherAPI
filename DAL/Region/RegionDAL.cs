using Microsoft.EntityFrameworkCore;

namespace WeatherAPI.DAL.Region;

public class RegionDAL : IRegionDAL
{
    public async Task AddRegion(Models.Region model, int accountId)
    {
        await using var context = new Context();
        var account = await context.Accounts.FindAsync(accountId);
        model.Account = account!;
        await context.Regions.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public async Task UpdateRegion(Models.Region model, int userId)
    {
        await using var context = new Context();
        model.Account = await context.Accounts.FindAsync(userId);
        await Task.Run(() =>
        {
            context.Regions.Update(model);
        });
        await context.SaveChangesAsync();
    }

    public async Task<Models.Region> GetRegionById(int id)
    {
        await using var context = new Context();
        return await context.Regions.FindAsync(id) ?? new Models.Region();
    }

    public async Task DeleteRegion(int id)
    {
        await using var context = new Context();
        var model = await GetRegionById(id);
        await Task.Run(() =>
        {
            context.Regions.Remove(model);
        });
        await context.SaveChangesAsync();
    }

    public async Task<Models.Region> GetRegionByLatLong(double latitude, double longitude)
    {
        await using var context = new Context();
        return await context.Regions.FirstOrDefaultAsync(
                   r => r.Longitude == longitude && r.Latitude == latitude) ??
                   new Models.Region();
    }

    public async Task<bool> IsRegionParent(string parentName)
    {
        await using var context = new Context();
        var modelFromDb = await context.Regions.FirstOrDefaultAsync(r => r.ParentRegion == parentName) ??
                          new Models.Region();
        return modelFromDb.Id != 0;
    }
}