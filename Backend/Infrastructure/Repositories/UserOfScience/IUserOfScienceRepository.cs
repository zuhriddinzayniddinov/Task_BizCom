using Topshiriq.Domain.Entities.UserOfSciences;

namespace Topshiriq.Infrastructure.Repositories.UserOfScience;

public interface IUserOfScienceRepository
{
    Task<StudentOfSciences> InsertStudentOfScienceAsync(StudentOfSciences studentOfSciences);
    Task<TeacherOfSciences> InsertTeacherOfSciencesAsync(TeacherOfSciences teacherOfSciences);
    Task<int> SaveChangesAsync();
    IQueryable<StudentOfSciences> StudentOfScienceAll(int userId);
    IQueryable<StudentOfSciences> StudentOfScienceByScienceId(int scienceId);
    IQueryable<StudentOfSciences> StudentOfScience();
    IQueryable<TeacherOfSciences> TeacherOfScienceAll(int userId);
    IQueryable<TeacherOfSciences> TeacherOfScience();
    Task<StudentOfSciences> UpdateStudendtOfScienceAsync(StudentOfSciences studentOfSciences);
}
