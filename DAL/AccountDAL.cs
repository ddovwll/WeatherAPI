using Microsoft.EntityFrameworkCore;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL;

public class AccountDAL : IAccountDAL
{
    public async Task SaveAsync(Account model)
    {
        await using var context = new Context();
        await context.Accounts.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public async Task<Account> FindByEmailAsync(string email)
    {
        await using var context = new Context();
        return await context.Accounts.FirstOrDefaultAsync(m => m.Email == email)
               ?? new Account();
    }

    public async Task<Account> FindByIdAsync(int? id)
    {
        if (id == null)
            return new Account();
        await using var context = new Context();
        return await context.Accounts.FindAsync(id) ?? new Account();
    }

    public async Task UpdateAsync(Account model)
    {
        await using var context = new Context();
        await Task.Run(() => context.Accounts.Update(model));
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Account model)
    {
        await using var context = new Context();
        await Task.Run(() => context.Accounts.Remove(model));
        await context.SaveChangesAsync();
    }

    public async Task<List<Account>> FindByParams(string? firstName, 
                                                    string? lastName, 
                                                    string? email, 
                                                    int form = 0, 
                                                    int size = 10)
    {
        await using var context = new Context();
        IQueryable<Account> query = context.Accounts;
        
        if (!string.IsNullOrEmpty(firstName))
            query = query.Where(u => u.FirstName.ToLower().Contains(firstName.ToLower()));
        if (!string.IsNullOrEmpty(lastName))
            query = query.Where(u => u.LastName.ToLower().Contains(lastName.ToLower()));
        if (!string.IsNullOrEmpty(email))
            query = query.Where(u => u.Email.ToLower().Contains(email.ToLower()));
        query = query.Skip(form).Take(size);

        return await query.ToListAsync();
    }
}