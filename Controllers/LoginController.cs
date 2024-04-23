using Microsoft.AspNetCore.Mvc;
using WeatherAPI.BLL;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.Controllers;

public class LoginController : ControllerBase
{
    private readonly IAccountBLL accountBll;
    private readonly IEncrypt encrypt;

    public LoginController(IAccountBLL accountBll, IEncrypt encrypt)
    {
        this.accountBll = accountBll;
        this.encrypt = encrypt;
    }

    [HttpPost]
    [Route("/login")]
    public async Task<IActionResult> Login([FromBody] Account model)
    {
        string cookieValue = Request.Cookies["UserId"];

        if (cookieValue != null)
            return new ContentResult
            {
                StatusCode = 200
            };
        
        if (!model.Validate())
            return Unauthorized();
        var modelFromDb = await accountBll.FindByEmailAsync(model.Email);
        if(encrypt.HashPassword(model.Password, modelFromDb.Salt)==modelFromDb.Password)
        {
            var response = new
            {
                id = modelFromDb.Id
            };
            
            CookieOptions options = new CookieOptions()
            {
                IsEssential = true,
                Expires = DateTime.Now.AddDays(31),
            };
            
            Response.Cookies.Append("UserId", modelFromDb.Id.ToString(), options);
            
            return Ok(response);
        }
        
        return Unauthorized();
    }
}