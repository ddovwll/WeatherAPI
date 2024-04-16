using WeatherAPI.DAL.Models;
using WeatherAPI.DAL.QueryModels;

namespace WeatherAPI.BLL;

public interface IAccountBLL
{
    Task SaveAsync(Account model);
    Task<Account> FindByEmailAsync(string email);
    Task<Account> FindByIdAsync(int? id);
    Task UpdateAsync(Account model);
    Task DeleteAsync(Account model);
    Task<List<AccountQueryModel>> GetAccountByParams(string firstName,
                            string lastName,
                            string email,
                            int form,
                            int size);
}