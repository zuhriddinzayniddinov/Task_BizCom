using Topshiriq.Domain.Entities.Sciences;
using Topshiriq.Domain.Enums;

namespace Topshiriq.Infrastructure.Repositories;

public interface IScienceRepository
{
    Task<Science> InsertAsync(Science science);
    IQueryable<Science> SelectAll();
    Task<Science> SelectByIdAsync(int scienceId);
    Task<Science> UpdateAsync(Science science);
    Science Delete(Science science);
    Task<int> SaveChangesAsync();
}
