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
            List<UserModel> dataTable = _userService.GetUserData();

            return Ok(dataTable);
        }

        [Authorize]
        [HttpPost]
        [Route("InsertData")]
        public IActionResult InsertUserData(UserModel userModel)
        {
            try
            {
                bool isSuccess = _userService.InsertUserData(userModel);
                if (isSuccess)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            // default return statement for cases where no exception is caught
            return StatusCode(500, "An unexpected error occurred.");
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateData")]
        public IActionResult UpdateUserData(UserModel userModel)
        {
            try
            {
                bool isSuccess = _userService.UpdateUserData(userModel);
                if (isSuccess)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            // default return statement for cases where no exception is caught
            return StatusCode(500, "An unexpected error occurred.");
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteData")]
        public IActionResult DeleteUserData(string userID)
        {
            try
            {
                bool isSuccess = _userService.DeleteUserData(userID);
                if (isSuccess)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            // default return statement for cases where no exception is caught
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}
