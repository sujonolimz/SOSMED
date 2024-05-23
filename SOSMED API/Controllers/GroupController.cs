using Microsoft.AspNetCore.Mvc;
using SOSMED_API.Models;
using SOSMED_API.Services;

namespace SOSMED_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [Route("GetData")]
        public IActionResult GetGroupData()
        {
            List<GroupModel> dataTable = _groupService.GetGroupData();

            return Ok(dataTable);
        }

        [HttpPost]
        [Route("InsertData")]
        public IActionResult InsertGroupData(GroupModel groupModel)
        {
            try
            {
                bool isSuccess = _groupService.InsertGroupData(groupModel);
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
        public IActionResult UpdateGroupData(GroupModel groupModel)
        {
            try
            {
                bool isSuccess = _groupService.UpdateGroupData(groupModel);
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
        public IActionResult DeleteGroupData(string groupID)
        {
            try
            {
                bool isSuccess = _groupService.DeleteGroupData(groupID);
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
