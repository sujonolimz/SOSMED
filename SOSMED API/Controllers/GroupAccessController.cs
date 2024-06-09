using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOSMED_API.Interface;
using SOSMED_API.Models;

namespace SOSMED_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupAccessController : ControllerBase
    {
        private readonly IGroupAccess _groupAccessService;

        public GroupAccessController(IGroupAccess groupAccessService)
        {
            _groupAccessService = groupAccessService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public IActionResult GetGroupAccessData()
        {
            var result = _groupAccessService.GetGroupAccessData();
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetDetailData")]
        public IActionResult GetGroupAccessDetailData(string groupID)
        {
            var result = _groupAccessService.GetGroupAccessDetailData(groupID);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("InsertData")]
        public IActionResult InsertGroupAccessData(IEnumerable<GroupAccessModel> groupAccessModelList)
        {
            var result = _groupAccessService.InsertGroupAccessData(groupAccessModelList);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteData")]
        public IActionResult DeleteGroupAccessData(string groupID)
        {
            var result = _groupAccessService.DeleteGroupAccessData(groupID);
            return Ok(result);
        }
    }
}
