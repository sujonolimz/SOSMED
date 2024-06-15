using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOSMED_API.Interface;
using SOSMED_API.Models;

namespace SOSMED_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public IActionResult GetFormData()
        {
            var result = _userService.GetUserData();
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("InsertData")]
        public IActionResult InsertUserData(UserModel userModel)
        {
            var result = _userService.InsertUserData(userModel);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateData")]
        public IActionResult UpdateUserData(UserModel userModel)
        {
            var result = _userService.UpdateUserData(userModel);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteData")]
        public IActionResult DeleteUserData(string userID)
        {
            var result = _userService.DeleteUserData(userID);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetTotalMAUs")]
        public IActionResult GetTotalMAUs()
        {
            var result = _userService.GetTotalMAUs();
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetUserLoginHistoryData")]
        public IActionResult GetUserLoginHistoryData(string year, string month)
        {
            var result = _userService.GetUserLoginHistoryData(year, month);
            return Ok(result);
        }
    }
}
