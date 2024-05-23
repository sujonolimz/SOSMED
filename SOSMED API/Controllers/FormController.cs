using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using SOSMED_API.Helpers;
using SOSMED_API.Models;
using SOSMED_API.Services;
using System.Data;
using System.Text.Json.Nodes;

namespace SOSMED_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpGet]
        [Route("GetData")]  
        public IActionResult GetFormData()
        {
            List<FormModel> dataTable = _formService.GetFormData();

            return Ok(dataTable);
        }

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
