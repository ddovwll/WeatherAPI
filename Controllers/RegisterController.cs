using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherAPI.BLL;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.Controllers;

public class RegisterController : ControllerBase
{
    private readonly IAccountBLL accountBll;

    public RegisterController(IAccountBLL accountBll)
    {
        this.accountBll = accountBll;
    }

    [HttpPost]
    [Route("/registration")]
    public async Task<IActionResult> RegisterAsync([FromBody] Account model)
    {
        string cookieValue = Request.Cookies["UserId"];

        if (cookieValue != null)
            return new ContentResult
            {
                StatusCode = 403
            };

        if (!model.Validate())
            return BadRequest();

        var modelFromDb = await accountBll.FindByEmailAsync(model.Email);

        if (modelFromDb.Id != 0)
            return Conflict();
        
        CookieOptions options = new CookieOptions()
        {
            IsEssential = true,
            Expires = DateTime.Now.AddDays(31),
        };
        
        await accountBll.SaveAsync(model);
        
        Response.Cookies.Append("UserId", model.Id.ToString(), options);
        
        var account = new
        {
            id = model.Id,
            firstName = model.FirstName,
            lastName = model.LastName,
            email = model.Email
        };
        
        return new ContentResult
        {
            StatusCode = 201,
            Content = JsonConvert.SerializeObject(account),
            ContentType = "application/json"
        };
    }
}