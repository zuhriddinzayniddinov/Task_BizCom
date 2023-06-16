using Microsoft.EntityFrameworkCore;
using Topshiriq.Domain.Entities.Users;
using Topshiriq.Domain.Entities.UserOfSciences;
using Topshiriq.Domain.Entities.Sciences;

namespace Topshiriq.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<StudentOfSciences> StudentsOfSciences { get; set; }
    public DbSet<TeacherOfSciences> TeachersOfSciences { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Science> Sciences { get; set; }
}