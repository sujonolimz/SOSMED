using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SOSMED_API.Interface;
using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using SOSMED_API.Services;

namespace SOSMED_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel model)
        {
            var result = _authService.Login(model.UserID, model.Password);
            return Ok(result);
        }

        [HttpPost]
        [Route("isHaveFormAccess")]
        public IActionResult isHaveFormAccess(CheckIsHaveAccess model)
        {
            var result = _authService.isHaveFormAccess(model);
            return Ok(result);
        }
    }
}
