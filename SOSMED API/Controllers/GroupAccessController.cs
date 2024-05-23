using Microsoft.AspNetCore.Mvc;
using SOSMED_API.Models;
using SOSMED_API.Services;

namespace SOSMED_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupAccessController : ControllerBase
    {
        private readonly IGroupAccessService _groupAccessService;

        public GroupAccessController(IGroupAccessService groupAccessService)
        {
            _groupAccessService = groupAccessService;
        }

        [HttpGet]
        [Route("GetData")]
        public IActionResult GetGroupAccessData()
        {
            List<GroupAccessModel> dataTable = _groupAccessService.GetGroupAccessData();

            return Ok(dataTable);
        }

        [HttpPost]
        [Route("InsertData")]
        public IActionResult InsertGroupAccessData(IEnumerable<GroupAccessModel> groupAccessModelList)
        {
            try
            {
                bool isSuccess = _groupAccessService.InsertGroupAccessData(groupAccessModelList);
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
        public IActionResult DeleteGroupAccessData(string groupID)
        {
            try
            {
                bool isSuccess = _groupAccessService.DeleteGroupAccessData(groupID);
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
