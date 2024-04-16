using Microsoft.AspNetCore.Http.HttpResults;
using WeatherAPI.DAL;
using WeatherAPI.DAL.Models;
using WeatherAPI.DAL.QueryModels;
using WeatherAPI.Helpers.Mappers;

namespace WeatherAPI.BLL;

public class AccountBLL : IAccountBLL
{
    private readonly IAccountDAL accountDal;
    private readonly IEncrypt encrypt;

    public AccountBLL(IAccountDAL accountDal, IEncrypt encrypt)
    {
        this.accountDal = accountDal;
        this.encrypt = encrypt;
    }

    public async Task SaveAsync(Account model)
    {
        model.Salt = Guid.NewGuid().ToString();
        model.Password = encrypt.HashPassword(model.Password, model.Salt);
        await accountDal.SaveAsync(model);
    }

    public async Task<Account> FindByEmailAsync(string email)
    {
        return await accountDal.FindByEmailAsync(email);
    }

    public async Task<Account> FindByIdAsync(int? id)
    {
        return await accountDal.FindByIdAsync(id);
    }

    public async Task UpdateAsync(Account model)
    {
        model.Salt = Guid.NewGuid().ToString();
        model.Password = encrypt.HashPassword(model.Password, model.Salt);
        await accountDal.UpdateAsync(model);
    }

    public async Task DeleteAsync(Account model)
    {
        await accountDal.DeleteAsync(model);
    }
    
    public async Task<List<AccountQueryModel>> GetAccountByParams(string firstName, string lastName, string email, int form, int size)
    {
        var models = await accountDal.FindByParams(firstName, lastName, email, form, size);
        var queryModels = await Task.Run(() =>
        {
            var queryModels = new List<AccountQueryModel>();
            foreach (var model in models)
            {
                queryModels.Add(AccountToAccountQueryModel.MapAccountToAccountQueryModel(model));
            }

            return queryModels.OrderBy(qm=>qm.Id).ToList();
        });

        return queryModels;
    }
}