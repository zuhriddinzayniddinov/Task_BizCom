using Topshiriq.Domain.Entities.Sciences;

namespace Topshiriq.Application.Services.Sciences;

public interface IScienceService
{
    Task<Science> CreateScienceAsync(Science science);
    IQueryable<Science> RetrieveAllSciences();
    Task<Science> RetrieveByIdAsync(int scienceId);
}
