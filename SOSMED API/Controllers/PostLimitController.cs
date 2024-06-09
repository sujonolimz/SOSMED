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
            var result = _postLimitService.GetPostLimitData();
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("InsertData")]
        public IActionResult InsertPostLimitData(PostLimitModel postLimitModel)
        {
            var result = _postLimitService.InsertPostLimitData(postLimitModel);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateData")]
        public IActionResult UpdatePostLimitData(PostLimitModel postLimitModel)
        {
            var result = _postLimitService.UpdatePostLimitData(postLimitModel);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteData")]
        public IActionResult DeletePostLimitData(string postLimitID)
        {
            var result = _postLimitService.DeletePostLimitData(postLimitID);
            return Ok(result);
        }
    }
}
