using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Topshiriq.Application.DataTransferObjects.UserOfScienceDto;
using Topshiriq.Application.DataTransferObjects.Users;
using Topshiriq.Domain.Entities.Sciences;
using Topshiriq.Domain.Entities.Users;
using Topshiriq.Domain.Enums;
using Topshiriq.Domain.Exceptions;
using Topshiriq.Infrastructure.Repositories;
using Topshiriq.Infrastructure.Repositories.UserOfScience;
using Topshiriq.Infrastructure.Repositories.Users;

namespace Topshiriq.Application.Services.UserOfSciences;

public class UserOfScienceService : IUserOfScienceService
{
    private readonly IUserOfScienceRepository _userOfScienceRepository;
    private readonly IUserRepository _userRepository;
    private readonly IScienceRepository _scienceRepository;

    public UserOfScienceService(
        IUserOfScienceRepository userOfScienceRepository,
        IUserRepository userRepository,
        IScienceRepository scienceRepository)
    {
        _userOfScienceRepository = userOfScienceRepository;
        _userRepository = userRepository;
        _scienceRepository = scienceRepository;
    }

    public async Task<object>? CreateUserOfScienceAsync(int scienceId, int userId)
    {
        var science = await _scienceRepository.SelectByIdAsync(scienceId);

        var user = await _userRepository.SelectByIdAsync(userId);

        UserRole userRole = user.Role;

        switch (userRole)
        {
            case UserRole.Student:
                var sOfScience = await _userOfScienceRepository.InsertStudentOfScienceAsync(new()
                {
                    StudentId = userId,
                    ScienceId = scienceId
                });
                return new StudentOfScienceDto(
                    sOfScience.Id,
                    science.Name,
                    science.Id,
                    user.Id,
                    user.FirstName,
                    sOfScience.Ball);
            case UserRole.Teacher:
                var tOfScience = await _userOfScienceRepository.InsertTeacherOfSciencesAsync(new()
                {
                    TeacherId = userId,
                    ScienceId = scienceId
                });
                return new TeacherOfScienceDto(
                    tOfScience.Id,
                    science.Name,
                    science.Id,
                    user.Id,
                    user.FirstName);
            case UserRole.Admin:
                break;
            default:
                break;
        }
        return null;
    }

    public IQueryable<StudentOfScienceDto> GetAllStudentsByScienceId(int scienceId)
    {
        var result = (from student in _userOfScienceRepository.StudentOfScienceByScienceId(scienceId)
                      join science in _scienceRepository.SelectAll() on student.ScienceId equals science.Id
                      join user in _userRepository.SelectAll() on student.StudentId equals user.Id
                      select new StudentOfScienceDto(
                          student.Id,
                          science.Name,
                          student.ScienceId,
                          user.Id,
                          user.FirstName,
                          student.Ball));
        return result;
    }

    public IQueryable<object> GetAllUserOfSciences(int userId)
    {
        var user = _userRepository.SelectByIdAsync(userId).Result;

        UserRole userRole = user.Role;

        switch (userRole)
        {
            case UserRole.Student:
                var resultS = (from studentOfScience in _userOfScienceRepository.StudentOfScienceAll(userId)
                              join science in _scienceRepository.SelectAll() on studentOfScience.ScienceId equals science.Id
                              select new StudentOfScienceDto(
                                  studentOfScience.Id,
                                  science.Name,
                                  studentOfScience.ScienceId,
                                  user.Id,
                                  user.FirstName,
                                  studentOfScience.Ball));
                return resultS;
            case UserRole.Teacher:
                var resultT = (from studentOfScience in _userOfScienceRepository.TeacherOfScienceAll(userId)
                              join science in _scienceRepository.SelectAll() on studentOfScience.ScienceId equals science.Id
                              select new TeacherOfScienceDto(
                                  studentOfScience.Id,
                                  science.Name,
                                  studentOfScience.ScienceId,
                                  user.Id,
                                  user.FirstName));
                return resultT;
            case UserRole.Admin:
                break;
            default:
                break;
        }
        return null;
    }

    public async Task<List<Science>> GetBallCountAsync(int userId, int minBall, int userCount)
    {
        var teacherOfScience = _userOfScienceRepository
            .TeacherOfScienceAll(userId);

        List<int> temp = new();

        foreach (var item in teacherOfScience)
        {
            var sos = _userOfScienceRepository
                .StudentOfScienceByScienceId(item.ScienceId)
                .Where(x=>x.Ball >= minBall);

            if (sos.Count() >= userCount)
            {
                temp.Add(item.ScienceId);
            }
        }

        List<Science> result = new();

        for (int i = 0;i<temp.Count;i++)
            result.Add(await _scienceRepository.SelectByIdAsync(temp[i]));

        return result;
    }

    public async Task<Science> GetMaxBallAsync(int userId)
    {
        var user = await _userRepository.SelectByIdAsync(userId);

        var maxBall = _userOfScienceRepository
                .StudentOfScienceAll(userId)
                .Max(x => x.Ball);

        var studentOfScience = await _userOfScienceRepository
                .StudentOfScienceAll(userId)
                .Where(x => x.Ball == maxBall)
                .FirstOrDefaultAsync()
                ?? throw new NotFoundException("Not found max ball");

        return await _scienceRepository
                .SelectByIdAsync(studentOfScience.ScienceId);
    }

    public async Task<Science> GetNormalBallAsync()
    {
        var studentOfScienceq = _userOfScienceRepository
                .StudentOfScience()
                .OrderBy(x => x.Ball);

        var count = studentOfScienceq.Count();

        var studentOfScience = await studentOfScienceq
                                    .Skip(count/2)
                                    .FirstOrDefaultAsync()
                                    ?? throw new NotFoundException("Not found Normal ball");

        return await _scienceRepository
                .SelectByIdAsync(studentOfScience.ScienceId);
    }

    public async Task<List<UserDto>> GetTeacherByMaxBallStudents(int ball)
    {
        var scienceIds = _userOfScienceRepository
                            .StudentOfScience()
                            .Where(s => s.Ball >= ball)
                            .Select(s => s.ScienceId);

        List<int> teacherIds = new();
        foreach (var item in scienceIds)
        {
            teacherIds.AddRange(_userOfScienceRepository
                .TeacherOfScience()
                .Where(t => t.ScienceId == item)
                .Select(x => x.TeacherId));
        }

        List<UserDto> teacher = new();

        foreach (var item in teacherIds)
        {
            teacher.Add(Map(await _userRepository.SelectByIdAsync(item)));
        }

        return teacher;
    }

    private UserDto Map(User user)
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

    public async Task<StudentOfScienceDto> UpdateAsync(StudentOfScienceDto studentOfScienceDto)
    {
        var studentOfScience = await _userOfScienceRepository.UpdateStudendtOfScienceAsync(new()
        {
            Id = studentOfScienceDto.id,
            ScienceId = studentOfScienceDto.scienceId,
            Ball = studentOfScienceDto.ball,
            StudentId = studentOfScienceDto.userId
        });

        return studentOfScienceDto;
    }
}
