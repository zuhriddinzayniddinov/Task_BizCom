using Topshiriq.Application.DataTransferObjects.Users;
using Topshiriq.Domain.Entities.Users;
using Topshiriq.Domain.Enums;
using Topshiriq.Domain.Exceptions;
using Topshiriq.Infrastructure.Authentication;
using Topshiriq.Infrastructure.Repositories.Users;
using Topshiriq.Domain.Enums;

namespace Topshiriq.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task<UserDto> CreateUserAsync(UserForCreationDto userForCreationDto)
    {
        var salt = Guid.NewGuid()
                        .ToString();

        var passwordHash =
            this._passwordHasher
                .Encrypt(
                    userForCreationDto.password,
                    salt);

        PhoneCompony phoneCompony =
            DeterminationPhoneCompony(userForCreationDto.phoneNumber);

        User user = new()
        {
            FirstName = userForCreationDto.firstName,
            LastName = userForCreationDto.lastName,
            Email = userForCreationDto.email,
            PhoneNumber = userForCreationDto.phoneNumber,
            PhoneCompony = phoneCompony,
            Birthdate = userForCreationDto.birthday,
            Salt = salt,
            PasswordHash = passwordHash,
            Role = userForCreationDto.role
        };

        user = await this._userRepository
            .InsertAsync(user);

        return UserToDto(user);
    }
    private PhoneCompony DeterminationPhoneCompony(string phoneNumber)
    {
        int code = int.Parse(phoneNumber.Substring(4, 2));

        return code switch
        {
            33 => PhoneCompony.Humans,
            95 or 99 => PhoneCompony.Uzmobile,
            90 or 91 => PhoneCompony.Beeline,
            88 or 97 => PhoneCompony.MobiUz,
            93 or 94 or 50 => PhoneCompony.Ucell,
            _ => throw new NoValidException()
        };
    }
    private static UserDto UserToDto(User user)
    {
        return new UserDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.Email,
            user.Birthdate,
            user.Role);
    }
    public async Task<UserDto> RemoveUserAsync(int userId)
    {
        User user = new()
        {
            Id = userId
        };

        return UserToDto(await _userRepository
            .DeleteAsync(user));
    }
    public async Task<UserDto> RetrieveUserByIdAsync(int userId)
    {
        var user = await _userRepository.SelectByIdAsync(userId);

        return UserToDto(user);
    }
    public IQueryable<UserDto> RetrieveUsers()
    {
        return _userRepository.SelectAll()
            .Select(u => UserToDto(u));
    }
    public IQueryable<UserDto> RetrieveUsersByAge(
        int age, UserRole role, string underOrForm)
    {
        var year = DateTime.UtcNow.Year;

        return _userRepository
            .SelectByRole(role)
            .Where(u=>
                underOrForm == "under"
                    ? year - u.Birthdate.Year <= age
                    : year - u.Birthdate.Year >= age)
            .Select(u=>UserToDto(u));
    }

    public IQueryable<UserDto> RetrieveUsersByBirthdayInterval(
        DateTime startDate, DateTime endDate)
    {
        return _userRepository
            .SelectByDataBirthdayInterval(startDate, endDate)
            .Select(u => UserToDto(u));
    }
    public IQueryable<UserDto> RetrieveUsersByName(string name)
    {
        return _userRepository
            .SelectByName(name)
            .Select(u => UserToDto(u));
    }

    public IQueryable<UserDto> RetrieveUsersByPhoneCompony(PhoneCompony phoneCompony)
    {
        return _userRepository
            .SelectPhoneCompany(phoneCompony)
            .Select(u => UserToDto(u));
    }
}