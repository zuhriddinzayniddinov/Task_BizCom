using Topshiriq.Domain.Entities.Users;
using Topshiriq.Domain.Enums;
using Topshiriq.Domain.Enums;

namespace Topshiriq.Infrastructure.Repositories.Users;

public interface IUserRepository
{
    
    Task<User> InsertAsync(User user);
    IQueryable<User> SelectAll();
    IQueryable<User> SelectByRole(UserRole role);
    IQueryable<User> SelectByDataBirthdayInterval(DateTime startDate, DateTime endDate);
    IQueryable<User> SelectPhoneCompany(PhoneCompony phoneCompony);
    IQueryable<User> SelectByName(string Name);
    Task<User> SelectByIdAsync(int id);
    Task<User> UpdateAsync(User user);
    Task<User> SelectByEmailAsync(string email);
    Task<User> SelectByPhoneAsync(string phoneNumber);
    Task<User> DeleteAsync(User user);
    Task<int> SaveChangesAsync();
}