using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Topshiriq.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("Everyone")]
        public string GetEveryone()
        {
            return "Everyone";
        }
        
        [Authorize(Roles = "Student")]
        [HttpGet("Student")]
        public string GetStudent()
        {
            return "Student";
        }
        
        [Authorize(Roles = "Teacher")]
        [HttpGet("Teacher")]
        public string GetTeacher()
        {
            return "Teacher";
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin")]
        public string GetAdmin()
        {
            return "Admin";
        }
    }
}
