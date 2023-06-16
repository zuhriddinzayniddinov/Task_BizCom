namespace Topshiriq.Application.DataTransferObjects.UserOfScienceDto;

public record StudentOfScienceDto(
    int id,
    string scienceName,
    int scienceId,
    int userId,
    string userFirstName,
    int ball);