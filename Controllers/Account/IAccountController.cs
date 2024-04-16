using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.Controllers;

public interface IAccountController
{
    Task<IActionResult> GetAccountById(int? id);
}