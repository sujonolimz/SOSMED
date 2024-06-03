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
            List<FormModel> dataTable = _formService.GetFormData();

            return Ok(dataTable);
        }

        [Authorize]
        [HttpPost]
        [Route("InsertData")]
        public IActionResult InsertFormData(FormModel formModel)
        {
            try
            {
                bool isSuccess = _formService.InsertFormData(formModel);
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
        public IActionResult UpdateFormData(FormModel formModel)
        {
            try
            {
                bool isSuccess = _formService.UpdateFormData(formModel);
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
        public IActionResult DeleteFormData(string formID)
        {
            try
            {
                bool isSuccess = _formService.DeleteFormData(formID);
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
