namespace Topshiriq.Application.DataTransferObjects.Authentication;

public record TokenDto(
    string firstName,
    string role,
    string accessToken,
    DateTime expireDate);