using Microsoft.AspNetCore.Mvc;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.Controllers;

public interface IRegisterController
{
    Task<IActionResult> RegisterAsync([FromBody] Account model);
}