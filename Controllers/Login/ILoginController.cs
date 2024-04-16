using Microsoft.AspNetCore.Mvc;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.Controllers;

public interface ILoginController
{
    Task<IActionResult> Login([FromBody] Account model);
}