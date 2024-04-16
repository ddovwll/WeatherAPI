using WeatherAPI.DAL.Models;
using WeatherAPI.DAL.QueryModels;

namespace WeatherAPI.Helpers.Mappers;

public static class AccountToAccountQueryModel
{
    public static AccountQueryModel MapAccountToAccountQueryModel(Account account)
    {
        return new AccountQueryModel()
        {
            Id = account.Id,
            Email = account.Email,
            FirstName = account.FirstName,
            LastName = account.LastName,
        };
    }
}