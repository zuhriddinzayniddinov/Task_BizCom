using Microsoft.EntityFrameworkCore;
using Topshiriq.Domain.Entities.Sciences;
using Topshiriq.Domain.Enums;
using Topshiriq.Domain.Exceptions;
using Topshiriq.Infrastructure.Contexts;

namespace Topshiriq.Infrastructure.Repositories.Sciences;

public class ScienceRepository : IScienceRepository
{
    private readonly AppDbContext _appDbContext;
    public ScienceRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Science> InsertAsync(Science science)
    {
        await _appDbContext.Sciences.AddAsync(science);

        await SaveChangesAsync();

        return science;
    }
    public Science Delete(Science science)
    {
        _appDbContext.Sciences.Remove(science);

        return science;
    }
    public IQueryable<Science> SelectAll()
    {
        return _appDbContext.Sciences;
    }
    public async Task<Science> SelectByIdAsync(int scienceId)
    {
        return await _appDbContext.Sciences
            .Where(sc =>
                sc.Id.Equals(scienceId))
            .Select(sc => sc)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Not Found {scienceId} id in Science");
    }
    public async Task<Science> UpdateAsync(Science science)
    {
        _appDbContext.Sciences.Entry(science).State = EntityState.Modified;

        await SaveChangesAsync();

        return science;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }
}
