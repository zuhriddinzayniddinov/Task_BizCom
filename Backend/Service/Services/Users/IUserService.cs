using Topshiriq.Application.DataTransferObjects.Users;
using Topshiriq.Domain.Enums;
using Topshiriq.Domain.Enums;

namespace Topshiriq.Application.Services.Users;

public interface IUserService
{
    IQueryable<UserDto> RetrieveUsers();
    IQueryable<UserDto> RetrieveUsersByAge(int age,UserRole role,string underOrForm);
    IQueryable<UserDto> RetrieveUsersByBirthdayInterval(DateTime startDate,DateTime endDate);
    IQueryable<UserDto> RetrieveUsersByPhoneCompony(PhoneCompony phoneCompony);
    IQueryable<UserDto> RetrieveUsersByName(string name);
    Task<UserDto> CreateUserAsync(UserForCreationDto userForCreationDto);
    Task<UserDto> RetrieveUserByIdAsync(int userId);
    Task<UserDto> RemoveUserAsync(int userId);
}