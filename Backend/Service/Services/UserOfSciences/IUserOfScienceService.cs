using Topshiriq.Application.DataTransferObjects.UserOfScienceDto;
using Topshiriq.Application.DataTransferObjects.Users;
using Topshiriq.Domain.Entities.Sciences;
using Topshiriq.Domain.Enums;

namespace Topshiriq.Application.Services.UserOfSciences;

public interface IUserOfScienceService
{
    Task<object>? CreateUserOfScienceAsync(int scienceId, int userId);
    IQueryable<StudentOfScienceDto> GetAllStudentsByScienceId(int scienceId);
    IQueryable<object> GetAllUserOfSciences(int userId);
    Task<StudentOfScienceDto> UpdateAsync(StudentOfScienceDto studentOfScienceDto);
    Task<Science> GetMaxBallAsync(int userId);
    Task<Science> GetNormalBallAsync();
    Task<List<Science>> GetBallCountAsync(int userId,int minBall,int userCount);
    Task<List<UserDto>> GetTeacherByMaxBallStudents(int ball);
}
