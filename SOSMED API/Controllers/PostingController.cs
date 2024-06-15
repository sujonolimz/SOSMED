using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using SOSMED_API.Interface;
using SOSMED_API.Models;

namespace SOSMED_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostingController : ControllerBase
    {
        private readonly IPosting _postingService;

        public PostingController(IPosting postingService)
        {
            _postingService = postingService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public IActionResult GetFormData(string userID)
        {
            var result = _postingService.GetPostingData(userID);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("InsertData")]
        public IActionResult InsertPostingData(PostingModel postingModel)
        {
            var result = _postingService.InsertPostingData(postingModel);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateData")]
        public IActionResult UpdatePostingData(PostingModel postingModel)
        {
            var result = _postingService.UpdatePostingData(postingModel);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteData")]
        public IActionResult DeletePostingData(string postingID)
        {
            var result = _postingService.DeletePostingData(postingID);
            return Ok(result);
        }
    }
}
