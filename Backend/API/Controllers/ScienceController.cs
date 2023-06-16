using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Topshiriq.Application.Services.Sciences;
using Topshiriq.Domain.Entities.Sciences;

namespace Topshiriq.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ScienceController : ControllerBase
{
    private readonly IScienceService _scienceService;

    public ScienceController(IScienceService scienceService)
    {
        _scienceService = scienceService;
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<Science> Create([FromBody]Science science)
    {
        return await _scienceService.CreateScienceAsync(science);
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_scienceService.RetrieveAllSciences());
    }
    [HttpGet("{scienceId:int}")]
    public async Task<IActionResult> GetById(int scienceId)
    {
        return Ok(await _scienceService.RetrieveByIdAsync(scienceId));
    }
}