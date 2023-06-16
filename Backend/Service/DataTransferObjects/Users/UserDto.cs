using Topshiriq.Domain.Enums;

namespace Topshiriq.Application.DataTransferObjects.Users;

public record UserDto(
    int id,
    string firstName,
    string lastName,
    string phoneNumber,
    string email,
    DateTime birthday,
    UserRole role);