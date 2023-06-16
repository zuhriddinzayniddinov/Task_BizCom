using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topshiriq.Application.DataTransferObjects.Users.SearchDto;
using Topshiriq.Application.Services.UserOfSciences;
using Topshiriq.Application.Services.Users;
using Topshiriq.Domain.Enums;

namespace Topshiriq.Api.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserOfScienceService _userOfScienceService;

    public AdminController(IUserService userService, IUserOfScienceService userOfScienceService)
    {
        _userService = userService;
        _userOfScienceService = userOfScienceService;
    }
    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
        return Ok(this._userService.RetrieveUsers());
    }
    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetByIdUsersAsync(int userId)
    {
        var userDto = await this._userService.RetrieveUserByIdAsync(userId);
        return Ok(userDto);
    }
    [HttpDelete("{userId:int}")]
    public async Task<IActionResult> DeleteUserAsync(int userId)
    {
        var userDto = await this._userService.RemoveUserAsync(userId);
        return Ok(userDto);
    }
    [HttpGet("users/name")]
    public IActionResult GetByNameUsers([FromQuery]string name)
    {
        var users = _userService.RetrieveUsersByName(name);

        return Ok(users);
    }
    [HttpGet("users/phone")]
    public IActionResult GetByPhoneComponyUsers([FromQuery] PhoneCompony phoneCompony)
    {
        var users = _userService.RetrieveUsersByPhoneCompony(phoneCompony);

        return Ok(users);
    }
    [HttpGet("users/age")]
    public IActionResult GetByAgeUsers([FromQuery]SearchAgeDto searchAgeDto)
    {
        var users = _userService
            .RetrieveUsersByAge(
            searchAgeDto.age,
            searchAgeDto.role,
            searchAgeDto.underOrForm);

        return Ok(users);
    }
    [HttpGet("users/birthday")]
    public IActionResult GetByBirthdayUsers([FromQuery] SearchBirthdayDateDto searchBirthdayDateDto)
    {
        var users = _userService
            .RetrieveUsersByBirthdayInterval(
            searchBirthdayDateDto.startDate,
            searchBirthdayDateDto.endDate);

        return Ok(users);
    }
    [HttpGet("science/{userId:int}")]
    public async Task<IActionResult> GetScienceByUserMaxBall(int userId)
    {
        return Ok(await _userOfScienceService.GetMaxBallAsync(userId));
    }
    [HttpGet("science/normal")]
    public async Task<IActionResult> GetScienceByNormalBall()
    {
        return Ok(await _userOfScienceService.GetNormalBallAsync());
    }

    [HttpGet("science/{userId:int}/search")]
    public async Task<IActionResult> GetScienceByUserMaxBallCount(
            int userId,
            [FromQuery]int minBall,
            [FromQuery]int minCount)
    {
        return Ok(await _userOfScienceService.GetBallCountAsync(userId,minBall,minCount));
    }

    [HttpGet("teachers/{ball:int}")]
    public async Task<IActionResult> GetTeachersByBall(int ball)
    {
        return Ok(await _userOfScienceService.GetTeacherByMaxBallStudents(ball));
    }
}