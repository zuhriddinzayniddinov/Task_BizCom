using Topshiriq.Domain.Enums;

namespace Topshiriq.Application.DataTransferObjects.Users.SearchDto;

public record SearchAgeDto(
    int age,
    UserRole role,
    string underOrForm);