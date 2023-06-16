using Microsoft.EntityFrameworkCore;
using Topshiriq.Domain.Entities.UserOfSciences;
using Topshiriq.Infrastructure.Contexts;

namespace Topshiriq.Infrastructure.Repositories.UserOfScience;

public class UserOfScienceRepository : IUserOfScienceRepository
{
    private readonly AppDbContext _appDbContext;

    public UserOfScienceRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<StudentOfSciences> InsertStudentOfScienceAsync(StudentOfSciences studentOfSciences)
    {
        await _appDbContext.StudentsOfSciences.AddAsync(studentOfSciences);

        await SaveChangesAsync();

        return studentOfSciences;
    }

    public async Task<TeacherOfSciences> InsertTeacherOfSciencesAsync(TeacherOfSciences teacherOfSciences)
    {
        await _appDbContext.TeachersOfSciences.AddAsync(teacherOfSciences);

        await SaveChangesAsync();

        return teacherOfSciences;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }

    public IQueryable<StudentOfSciences> StudentOfScience()
    {
        return _appDbContext.StudentsOfSciences;
    }

    public IQueryable<StudentOfSciences> StudentOfScienceAll(int userId)
    {
        return _appDbContext.StudentsOfSciences
            .Where(s=>s.StudentId == userId);
    }

    public IQueryable<StudentOfSciences> StudentOfScienceByScienceId(int scienceId)
    {
        return _appDbContext.StudentsOfSciences.Where(s => s.ScienceId == scienceId);
    }

    public IQueryable<TeacherOfSciences> TeacherOfScience()
    {
        return _appDbContext.TeachersOfSciences;
    }

    public IQueryable<TeacherOfSciences> TeacherOfScienceAll(int userId)
    {
        return _appDbContext.TeachersOfSciences
            .Where(t=>t.TeacherId == userId);
    }

    public async Task<StudentOfSciences> UpdateStudendtOfScienceAsync(StudentOfSciences studentOfSciences)
    {
        _appDbContext.StudentsOfSciences.Entry(studentOfSciences).State = EntityState.Modified;

        await SaveChangesAsync();

        return studentOfSciences;
    }
}
