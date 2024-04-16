using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL;

public interface IAccountDAL
{
    Task SaveAsync(Account model);
    Task<Account> FindByEmailAsync(string email);
    Task<Account> FindByIdAsync(int? id);
    Task UpdateAsync(Account model);
    Task DeleteAsync(Account model);
    Task<List<Account>> FindByParams(string? firstName,
                                    string? lastName,
                                    string? email,
                                    int form = 0,
                                    int size = 10);
}