using Topshiriq.Domain.Entities.Sciences;
using Topshiriq.Infrastructure.Repositories;

namespace Topshiriq.Application.Services.Sciences;

public class ScienceService : IScienceService
{
    private readonly IScienceRepository _scienceRepository;

    public ScienceService(IScienceRepository scienceRepository)
    {
        _scienceRepository = scienceRepository;
    }

    public async Task<Science> CreateScienceAsync(Science science)
    {
        await _scienceRepository.InsertAsync(science);

        return science;
    }

    public IQueryable<Science> RetrieveAllSciences()
    {
        return _scienceRepository.SelectAll();
    }

    public async Task<Science> RetrieveByIdAsync(int scienceId)
    {
        return await _scienceRepository.SelectByIdAsync(scienceId);
    }
}
