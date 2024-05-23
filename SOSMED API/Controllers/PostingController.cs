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
    }
}
