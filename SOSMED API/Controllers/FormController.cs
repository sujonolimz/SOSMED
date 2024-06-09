using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOSMED_API.Interface;
using SOSMED_API.Models;

namespace SOSMED_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IForm _formService;

        public FormController(IForm formService)
        {
            _formService = formService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetData")]  
        public IActionResult GetFormData()
        {
            var result = _formService.GetFormData();
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("InsertData")]
        public IActionResult InsertFormData(FormModel formModel)
        {
            var result = _formService.InsertFormData(formModel);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateData")]
        public IActionResult UpdateFormData(FormModel formModel)
        {
            var result = _formService.UpdateFormData(formModel);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteData")]
        public IActionResult DeleteFormData(string formID)
        {
            var result = _formService.DeleteFormData(formID);
            return Ok(result);
        }
    }
}
