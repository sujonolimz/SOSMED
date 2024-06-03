using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOSMED_API.Interface;
using SOSMED_API.Models;

namespace SOSMED_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostLimitController : ControllerBase
    {
        private readonly IPostLimit _postLimitService;

        public PostLimitController(IPostLimit postLimitService)
        {
            _postLimitService = postLimitService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public IActionResult GetPostLimitData()
        {
            List<PostLimitModel> dataTable = _postLimitService.GetPostLimitData();

            return Ok(dataTable);
        }

        [Authorize]
        [HttpPost]
        [Route("InsertData")]
        public IActionResult InsertPostLimitData(PostLimitModel postLimitModel)
        {
            try
            {
                bool isSuccess = _postLimitService.InsertPostLimitData(postLimitModel);
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
        public IActionResult UpdatePostLimitData(PostLimitModel postLimitModel)
        {
            try
            {
                bool isSuccess = _postLimitService.UpdatePostLimitData(postLimitModel);
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
        public IActionResult DeletePostLimitData(string postLimitID)
        {
            try
            {
                bool isSuccess = _postLimitService.DeletePostLimitData(postLimitID);
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
