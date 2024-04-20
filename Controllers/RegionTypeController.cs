using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherAPI.BLL;
using WeatherAPI.BLL.RegionType;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.Controllers;

public class RegionTypeController : ControllerBase
{
    private readonly IRegionTypeBLL regionTypeBll;
    private readonly IAuth auth;

    public RegionTypeController(IRegionTypeBLL regionTypeBll, IAuth auth)
    {
        this.regionTypeBll = regionTypeBll;
        this.auth = auth;
    }

    [HttpGet]
    [Route("/region/types/{typeId:int?}")]
    public async Task<IActionResult> GetRegion(int? typeId)
    {
        if (!auth.Authenticate(Request.Cookies))
            return Unauthorized();
        if (typeId == null || typeId <= 0)
            return BadRequest();
        var modelFromDb = await regionTypeBll.GetRegionTypeById(typeId.Value);
        if (modelFromDb.Id == 0)
            return NotFound();
        return Ok(modelFromDb);
    }

    [HttpPost]
    [Route("/region/types")]
    public async Task<IActionResult> AddRegionType([FromBody] RegionType model)
    {
        if (!auth.Authenticate(Request.Cookies))
            return Unauthorized();
        if (!model.Validate())
            return BadRequest();
        var modelFromDb = await regionTypeBll.GetRegionTypeByType(model.Type);
        if (modelFromDb.Id != 0)
            return Conflict();
        await regionTypeBll.AddRegionType(model);
        return new ContentResult
        {
            StatusCode = 201,
            Content = JsonConvert.SerializeObject(model),
            ContentType = "application/json"
        };
    }

    [HttpPut]
    [Route("/region/types/{typeId:int?}")]
    public async Task<IActionResult> UpdateRegionType(int? typeId, [FromBody] RegionType model)
    {
        if (!auth.Authenticate(Request.Cookies))
            return Unauthorized();
        if (typeId == null || typeId <= 0 || !model.Validate())
            return BadRequest();
        if ((await regionTypeBll.GetRegionTypeByType(model.Type)).Id != 0)
            return Conflict();
        var modelFromDb = await regionTypeBll.GetRegionTypeById(typeId.Value);
        if (modelFromDb.Id == 0)
            return NotFound();
        model.Id = modelFromDb.Id;
        await regionTypeBll.UpdateRegionType(model);
        return Ok(model);
    }

    [HttpDelete]
    [Route("/region/types/{typeId:int?}")]
    public async Task<IActionResult> DeleteRegionType(int? typeId)
    {
        if (!auth.Authenticate(Request.Cookies))
            return Unauthorized();
        if (typeId == null || typeId <= 0 
                           || await regionTypeBll.IsRegionWithTypeExists(typeId.Value))
            return BadRequest();
        var modelFromDb = await regionTypeBll.GetRegionTypeById(typeId.Value);
        if (modelFromDb.Id == 0)
            return NotFound();
        await regionTypeBll.DeleteRegionType(typeId.Value);
        return Ok();
    }
}