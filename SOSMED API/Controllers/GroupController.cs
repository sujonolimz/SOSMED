using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOSMED_API.Interface;
using SOSMED_API.Models;
using SOSMED_API.Services;

namespace SOSMED_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroup _groupService;

        public GroupController(IGroup groupService)
        {
            _groupService = groupService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public IActionResult GetGroupData()
        {
            var result = _groupService.GetGroupData();
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("InsertData")]
        public IActionResult InsertGroupData(GroupModel groupModel)
        {
            var result = _groupService.InsertGroupData(groupModel);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateData")]
        public IActionResult UpdateGroupData(GroupModel groupModel)
        {
            var result = _groupService.UpdateGroupData(groupModel);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteData")]
        public IActionResult DeleteGroupData(string groupID)
        {
            var result = _groupService.DeleteGroupData(groupID);
            return Ok(result);
        }
    }
}
