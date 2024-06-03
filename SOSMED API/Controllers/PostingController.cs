using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetFormData()
        {
            List<PostingModel> dataTable = _postingService.GetPostingData();

            return Ok(dataTable);
        }

        [Authorize]
        [HttpPost]
        [Route("InsertData")]
        public IActionResult InsertPostingData(PostingModel postingModel)
        {
            try
            {
                bool isSuccess = _postingService.InsertPostingData(postingModel);
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
        public IActionResult UpdatePostingData(PostingModel postingModel)
        {
            try
            {
                bool isSuccess = _postingService.UpdatePostingData(postingModel);
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
        public IActionResult DeletePostingData(string postingID)
        {
            try
            {
                bool isSuccess = _postingService.DeletePostingData(postingID);
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
