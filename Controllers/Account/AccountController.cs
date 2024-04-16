using Microsoft.AspNetCore.Mvc;
using WeatherAPI.BLL;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.Controllers;

public class AccountController : ControllerBase, IAccountController
{
    private readonly IAccountBLL accountBll;
    private readonly IAuth auth; 

    public AccountController(IAccountBLL accountBll, IAuth auth)
    {
        this.accountBll = accountBll;
        this.auth = auth;
    }

    [HttpGet]
    [Route("/accounts/{id:int?}")]
    public async Task<IActionResult> GetAccountById(int? id)
    {
        if (!auth.Authenticate(Request.Cookies))
            return Unauthorized();
        
        if (id == null || id <= 0)
            return BadRequest();
        
        var modelFromDb = await accountBll.FindByIdAsync(id);
        if (modelFromDb.Id == 0)
            return NotFound();
        
        var body = new
        {
            id = modelFromDb.Id,
            firstName = modelFromDb.FirstName,
            lastName = modelFromDb.LastName,
            email = modelFromDb.Email
        };

        return Ok(body);
    }
    
    [HttpGet]
    [Route("/accounts/search")]
    public async Task<IActionResult> GetAccountByParams(string? firstName,
        string? lastName,
        string? email,
        int? form = 0,
        int? size = 10)
    {
        if (!auth.Authenticate(Request.Cookies))
            return Unauthorized();
        if (form < 0 || size <= 0)
            return BadRequest();
        var modelsFromDb = await accountBll.GetAccountByParams(
            firstName,
            lastName,
            email,
            form.Value,
            size.Value);
        return Ok(modelsFromDb);
    }

    [HttpPut]
    [Route("/accounts/{accountId:int}")]
    public async Task<IActionResult> UpdateAccount(int accountId, [FromBody] Account newModel)
    {
        if (!auth.Authenticate(Request.Cookies))
            return Unauthorized();
        if (accountId == null || accountId <= 0 || !newModel.Validate())
            return BadRequest();
        var idFromCookie = int.Parse(Request.Cookies["UserId"]);
        var modelFromDb = await accountBll.FindByIdAsync(idFromCookie);
        if (modelFromDb.Id == 0 || idFromCookie != accountId)
            return Forbid();
        if ((await accountBll.FindByEmailAsync(newModel.Email)).Id != 0)
            return Conflict();
        newModel.Id = accountId;
        await accountBll.UpdateAsync(newModel);
        var updatedModel = new
        {
            id = newModel.Id,
            firstName = newModel.FirstName,
            lastName = newModel.LastName,
            email = newModel.Email
        };
        return Ok(updatedModel);
    }

    [HttpDelete]
    [Route("/accounts/{accountId:int}")]
    public async Task<IActionResult> DeleteAccount(int accountId)
    {
        if (accountId == null || accountId <= 0)
            return BadRequest();
        var idFromCookie = int.Parse(Request.Cookies["UserId"]);
        if (!auth.Authenticate(Request.Cookies))
            return Unauthorized();
        var modelFromDb = await accountBll.FindByIdAsync(accountId);
        if (idFromCookie != accountId || modelFromDb.Id == 0)
            return Forbid();
        await accountBll.DeleteAsync(modelFromDb);
        return Ok();
    }
}