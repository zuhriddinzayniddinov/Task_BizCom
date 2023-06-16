using Topshiriq.Domain.Enums;

namespace Topshiriq.Application.DataTransferObjects.Users;

public record UserForCreationDto(
    string firstName,
    string lastName,
    string phoneNumber,
    string email,
    DateTime birthday,
    string password,
    UserRole role);