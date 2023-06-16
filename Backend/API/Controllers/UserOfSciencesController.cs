using Microsoft.AspNetCore.Mvc;
using Topshiriq.Application.DataTransferObjects.UserOfScienceDto;
using Topshiriq.Application.Services.UserOfSciences;
using Topshiriq.Domain.Enums;

namespace Topshiriq.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserOfSciencesController : ControllerBase
{
    private readonly IUserOfScienceService _userOfScienceService;

    public UserOfSciencesController(IUserOfScienceService userOfScienceService)
    {
        _userOfScienceService = userOfScienceService;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]int scienceId)
    {
        var userId = HttpContext
                        .User
                        .Claims
                        .Where(c =>
                            c.Type == "Id")
                        .Select(c =>
                            int.Parse(c.Value))
                        .First();

        return Ok(await _userOfScienceService.CreateUserOfScienceAsync(scienceId,userId));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var userId = HttpContext
                        .User
                        .Claims
                        .Where(c =>
                            c.Type == "Id")
                        .Select(c =>
                            int.Parse(c.Value))
                        .First();

        return Ok(_userOfScienceService.GetAllUserOfSciences(userId));
    }

    [HttpGet("{scienceId:int}")]
    public IActionResult GetByIdAll(int scienceId)
    {
        return Ok(_userOfScienceService.GetAllStudentsByScienceId(scienceId));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody]StudentOfScienceDto studentOfScienceDto)
    {
        return Ok(await _userOfScienceService.UpdateAsync(studentOfScienceDto));
    }
}
