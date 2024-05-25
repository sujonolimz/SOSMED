using Microsoft.AspNetCore.Mvc;
using SOSMED_API.Models;
using SOSMED_API.Services;

namespace SOSMED_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostingController : ControllerBase
    {
        private readonly IPostingService _postingService;

        public PostingController(IPostingService postingService)
        {
            _postingService = postingService;
        }

        [HttpGet]
        [Route("GetData")]
        public IActionResult GetFormData()
        {
            List<PostingModel> dataTable = _postingService.GetPostingData();

            return Ok(dataTable);
        }

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
