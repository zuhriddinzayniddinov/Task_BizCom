namespace Topshiriq.Application.DataTransferObjects.UserOfScienceDto;

public record TeacherOfScienceDto(
    int id,
    string scienceName,
    int scienceId,
    int userId,
    string userFirstName);