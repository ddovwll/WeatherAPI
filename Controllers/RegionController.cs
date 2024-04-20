using Microsoft.AspNetCore.Mvc;
using WeatherAPI.BLL;
using WeatherAPI.BLL.Region;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.Controllers;

public class RegionController : ControllerBase
{
    private readonly IRegionBLL regionBLL;
    private readonly IAuth auth;

    public RegionController(IRegionBLL regionBLL, IAuth auth)
    {
        this.auth = auth;
        this.regionBLL = regionBLL;
    }

    [HttpGet]
    [Route("/region/{regionId:int}")]
    public async Task<IActionResult> GetRegionById(int regionId)
    {
        if (!auth.Authenticate(Request.Cookies))
            return Unauthorized();
        if (regionId == null || regionId <= 0)
            return BadRequest();
        var modelFromDb = await regionBLL.GetRegionById(regionId);
        if (modelFromDb.Id == 0)
            return NotFound();
        var resultModel = new
        {
            id = modelFromDb.Id,
            regionType = modelFromDb.RegionType,
            accountId = modelFromDb.AccountId,
            name = modelFromDb.Name,
            parentRegion = modelFromDb.ParentRegion,
            latitude = modelFromDb.Latitude,
            longitude = modelFromDb.Longitude
        };
        return Ok(resultModel);
    }

    [HttpPost]
    [Route("/region")]
    public async Task<IActionResult> AddRegion([FromBody] Region model)
    {
        if (!auth.Authenticate(Request.Cookies))
            return Unauthorized();
        if (!model.Validate())
            return BadRequest();
        if ((await regionBLL.GetRegionByLatLong(model.Latitude, model.Longitude)).Id != 0)
            return Conflict();
        int userId = int.Parse(Request.Cookies["UserId"]!);
        await regionBLL.AddRegion(model, userId, model.RegionType);
        var resultModel = new
        {
            id = model.Id,
            name = model.Name,
            parentRegion = model.ParentRegion,
            regionType = model.RegionType,
            latitude = model.Latitude,
            longitude = model.Longitude
        };
        return Ok(resultModel);
    }

    [HttpPut]
    [Route("/region/{regionId:int}")]
    public async Task<IActionResult> UpdateRegion(int regionId, [FromBody] Region model)
    {
        if (!auth.Authenticate(Request.Cookies))
            return Unauthorized();
        if (!model.Validate())
            return BadRequest();
        if ((await regionBLL.GetRegionById(regionId)).Id == 0)
            return NotFound();
        if ((await regionBLL.GetRegionByLatLong(model.Latitude, model.Longitude)).Id != 0)
            return Conflict();
        int userId = int.Parse(Request.Cookies["UserId"]!);
        model.Id = regionId;
        await regionBLL.UpdateRegion(model, userId, model.RegionType);
        var resultModel = new
        {
            id = model.Id,
            name = model.Name,
            parentRegion = model.ParentRegion,
            regionType = model.RegionType,
            latitude = model.Latitude,
            longitude = model.Longitude
        };
        return Ok(resultModel);
    }

    [HttpDelete]
    [Route("/region/{regionId:int}")]
    public async Task<IActionResult> DeleteRegion(int regionId)
    {
        if (!auth.Authenticate(Request.Cookies))
            return Unauthorized();
        if (regionId == null || regionId <= 0)
            return BadRequest();
        var currentModel = await regionBLL.GetRegionById(regionId);
        if (currentModel.Id == 0)
            return NotFound();
        if (await regionBLL.IsRegionParent(currentModel.Name))
            return BadRequest();
        await regionBLL.DeleteRegion(regionId);
        return Ok();
    }
}