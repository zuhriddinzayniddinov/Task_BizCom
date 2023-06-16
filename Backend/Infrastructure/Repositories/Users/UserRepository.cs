using Microsoft.EntityFrameworkCore;
using Topshiriq.Domain.Entities.Users;
using Topshiriq.Domain.Enums;
using Topshiriq.Domain.Exceptions;
using Topshiriq.Infrastructure.Contexts;
using Topshiriq.Domain.Enums;

namespace Topshiriq.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<User> InsertAsync(User user)
    {
        _appDbContext.Users.Add(user);
        await SaveChangesAsync();
        return user;
    }
    public async Task<User> DeleteAsync(User user)
    {
        _appDbContext.Users.Remove(user);
        await SaveChangesAsync();
        return user;
    }
    public IQueryable<User> SelectAll()
    {
        return _appDbContext.Users;
    }
    public async Task<User> SelectByEmailAsync(string email)
    {
        return await _appDbContext.Users
            .Where(u => u.Email == email)
            .Select(u => u)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Not Found {email} in User");
    }
    public async Task<User> SelectByIdAsync(int id)
    {
        return await _appDbContext.Users
            .Where(u => u.Id == id)
            .Select(u => u)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Not Found {id} in User");
    }
    public IQueryable<User> SelectByName(string Name)
    {
        return _appDbContext.Users
            .Where(u =>
                (u.FirstName.IndexOf(Name) != -1 ||
                u.LastName.IndexOf(Name) != -1) &&
                u.Role == UserRole.Student)
            .Select(u => u)
            ?? throw new NotFoundException($"Not Found {Name} in User");
    }
    public async Task<User> SelectByPhoneAsync(string phoneNumber)
    {
        return await _appDbContext.Users
            .Where(u => u.PhoneNumber == phoneNumber)
            .Select(u => u)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Not Found {phoneNumber} in User");
    }
    public IQueryable<User> SelectPhoneCompany(PhoneCompony phoneCompony)
    {
        return _appDbContext.Users
            .Where(u => u.PhoneCompony == phoneCompony)
            .Select(u => u)
            ?? throw new NotFoundException($"Not Found {phoneCompony} in User");
    }
    public async Task<User> UpdateAsync(User user)
    {
        _appDbContext.Users.Entry(user).State = EntityState.Modified;
        await SaveChangesAsync();
        return user;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await this._appDbContext
            .SaveChangesAsync();
    }

    public IQueryable<User> SelectByRole(UserRole role)
    {
        return _appDbContext.Users
            .Where(u => u.Role == role)
            .Select(u => u);
    }

    public IQueryable<User> SelectByDataBirthdayInterval(
        DateTime startDate,
        DateTime endDate)
    {
        return _appDbContext.Users
            .Where(u => u.Birthdate.DayOfYear >= startDate.DayOfYear
                        && u.Birthdate.DayOfYear <= endDate.DayOfYear)
            .Select(u => u)
            ?? throw new NotFoundException($"Not Found {startDate} - {endDate} interval birthday in User");
    }
}